using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CyberSecurityAwarenessBot
{
    public partial class MainWindow : Window
    {
        private Chatbot chatbot;
        private ApplicationDbContext dbContext;
        private QuizManager quizManager;
        private ActivityLogger activityLogger;
        private AudioPlayer audioPlayer;
        private bool isQuizActive = false;
        private bool isAudioEnabled = true;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComponents();
            ShowWelcomeMessage();
            LoadTasks();
            LoadActivityLog();
            txtMessage.Focus();

            if (audioPlayer != null)
            {
                audioPlayer.PlayGreeting();
            }
        }

        private void InitializeComponents()
        {
            chatbot = new Chatbot();
            dbContext = new ApplicationDbContext();
            quizManager = new QuizManager();
            activityLogger = ActivityLogger.Instance;
            audioPlayer = AudioPlayer.Instance;

            if (activityLogger != null)
            {
                activityLogger.LogAction("Application started - Part 3");
            }

            UpdateAudioStatus();
        }

        private void ShowWelcomeMessage()
        {
            string welcome = "Hello. I'm your Cybersecurity Assistant - Part 3." + Environment.NewLine + Environment.NewLine +
                "I can help you with:" + Environment.NewLine +
                "- Cybersecurity topics (phishing, passwords, malware)" + Environment.NewLine +
                "- Task management with reminders (using SQLite)" + Environment.NewLine +
                "- Cybersecurity quizzes" + Environment.NewLine +
                "- Activity tracking" + Environment.NewLine +
                "- Audio effects" + Environment.NewLine + Environment.NewLine +
                "Try typing: 'Help me with tasks' or 'Start a quiz'";

            AddBotMessage(welcome);
        }

        // Chat Methods
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        private void SendMessage()
        {
            string userInput = txtMessage.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput))
                return;

            AddUserMessage(userInput);
            txtMessage.Clear();

            if (isQuizActive && quizManager != null)
            {
                ProcessQuizAnswer(userInput);
                return;
            }

            if (chatbot == null) return;

            string response = chatbot.ProcessInput(userInput);

            if (response == "TASK_ADD")
            {
                HandleTaskAddition(userInput);
                return;
            }
            else if (response == "QUIZ_START")
            {
                StartQuiz();
                return;
            }
            else if (response == "ACTIVITY_SHOW")
            {
                ShowActivityLog();
                return;
            }

            AddBotMessage(response);
            ScrollToBottom();

            if (isAudioEnabled && audioPlayer != null)
            {
                audioPlayer.PlayNotification();
            }
        }

        // Task Management
        private void HandleTaskAddition(string input)
        {
            if (dbContext == null) return;

            string taskTitle = "Cybersecurity Task";
            string taskDescription = "Review and improve your security settings";

            if (input.Contains("two-factor") || input.Contains("2fa"))
            {
                taskTitle = "Enable Two-Factor Authentication";
                taskDescription = "Set up 2FA on all important accounts";
            }
            else if (input.Contains("password"))
            {
                taskTitle = "Update Password";
                taskDescription = "Change passwords and enable strong security";
            }
            else if (input.Contains("privacy"))
            {
                taskTitle = "Review Privacy Settings";
                taskDescription = "Check and update privacy settings on social media";
            }
            else if (input.Contains("backup"))
            {
                taskTitle = "Create Backup";
                taskDescription = "Backup important files and data";
            }
            else if (input.Contains("phishing"))
            {
                taskTitle = "Learn About Phishing";
                taskDescription = "Study how to identify and avoid phishing attacks";
            }
            else if (input.Contains("update"))
            {
                taskTitle = "Update Software";
                taskDescription = "Check and install software updates";
            }

            string reminder = null;
            if (input.Contains("remind") || input.Contains("reminder"))
            {
                if (input.Contains("day") || input.Contains("days"))
                {
                    if (input.Contains("1 day") || input.Contains("one day"))
                        reminder = "Reminder set for tomorrow";
                    else if (input.Contains("7") || input.Contains("seven"))
                        reminder = "Reminder set for 7 days from now";
                    else
                        reminder = "Reminder set for 3 days from now";
                }
                else if (input.Contains("week"))
                {
                    reminder = "Reminder set for 1 week from now";
                }
                else if (input.Contains("month"))
                {
                    reminder = "Reminder set for 1 month from now";
                }
                else
                {
                    reminder = "Reminder set for tomorrow";
                }
            }

            var task = new TaskItem
            {
                Title = taskTitle,
                Description = taskDescription,
                Reminder = reminder ?? string.Empty,
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };

            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            if (activityLogger != null)
            {
                activityLogger.LogAction("Task added: " + taskTitle + (reminder != null ? " (" + reminder + ")" : ""));
            }

            LoadTasks();

            string response = "Task added: " + taskTitle + Environment.NewLine +
                "Description: " + taskDescription + Environment.NewLine +
                (reminder != null ? "Reminder: " + reminder : "No reminder set");

            AddBotMessage(response);
            ScrollToBottom();

            if (isAudioEnabled && audioPlayer != null)
            {
                audioPlayer.PlaySuccess();
            }
        }

        private void LoadTasks()
        {
            try
            {
                if (dbContext == null) return;
                var tasks = dbContext.Tasks.OrderByDescending(t => t.CreatedAt).ToList();
                lstTasks.ItemsSource = null;
                lstTasks.ItemsSource = tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading tasks: " + ex.Message);
            }
        }

        private void btnCompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (dbContext == null) return;

            Button btn = sender as Button;
            TaskItem task = btn?.Tag as TaskItem;
            if (task != null)
            {
                var existingTask = dbContext.Tasks.Find(task.Id);
                if (existingTask != null)
                {
                    existingTask.IsCompleted = true;
                    dbContext.SaveChanges();

                    if (activityLogger != null)
                    {
                        activityLogger.LogAction("Task completed: " + task.Title);
                    }

                    LoadTasks();
                    AddBotMessage("Task " + task.Title + " marked as completed.");
                    ScrollToBottom();

                    if (isAudioEnabled && audioPlayer != null)
                    {
                        audioPlayer.PlayTaskComplete();
                    }
                }
            }
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (dbContext == null) return;

            Button btn = sender as Button;
            TaskItem task = btn?.Tag as TaskItem;
            if (task != null)
            {
                var existingTask = dbContext.Tasks.Find(task.Id);
                if (existingTask != null)
                {
                    dbContext.Tasks.Remove(existingTask);
                    dbContext.SaveChanges();

                    if (activityLogger != null)
                    {
                        activityLogger.LogAction("Task deleted: '" + task.Title + "'");
                    }

                    LoadTasks();
                    AddBotMessage("Task '" + task.Title + "' deleted.");
                    ScrollToBottom();

                    if (isAudioEnabled && audioPlayer != null)
                    {
                        audioPlayer.PlayNotification();
                    }
                }
            }
        }

        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            AddBotMessage("Your Tasks:" + Environment.NewLine + GetTaskList());
            ScrollToBottom();
        }

        private string GetTaskList()
        {
            if (dbContext == null) return "Database not available.";

            var tasks = dbContext.Tasks.OrderByDescending(t => t.CreatedAt).ToList();

            if (tasks == null || tasks.Count == 0)
                return "No tasks found. Add a task like: 'Add task to enable 2FA'";

            string result = "";
            int count = 1;
            foreach (var task in tasks)
            {
                result += count + ". " + (task.IsCompleted ? "[Completed] " : "[Pending] ") + task.Title + Environment.NewLine;
                result += "   " + task.Description + Environment.NewLine;
                if (!string.IsNullOrEmpty(task.Reminder))
                    result += "   Reminder: " + task.Reminder + Environment.NewLine;
                count++;
            }
            return result;
        }

        // Quiz Methods
        private void btnStartQuiz_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz();
        }

        private void StartQuiz()
        {
            if (quizManager == null) return;

            if (isQuizActive)
            {
                AddBotMessage("A quiz is already in progress. Answer the current question.");
                return;
            }

            isQuizActive = true;
            quizManager.StartQuiz();

            if (activityLogger != null)
            {
                activityLogger.LogAction("Quiz started");
            }

            AddBotMessage("Starting Cybersecurity Quiz!" + Environment.NewLine + Environment.NewLine + quizManager.GetCurrentQuestion());
            ScrollToBottom();
            txtQuizScore.Text = "Score: 0/" + quizManager.TotalQuestions;
            txtQuizFeedback.Text = "Answer the question in the chat.";

            if (isAudioEnabled && audioPlayer != null)
            {
                audioPlayer.PlayNotification();
            }
        }

        private void btnQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (quizManager == null) return;

            if (!isQuizActive)
            {
                AddBotMessage("Ready for a quiz? Type 'Start quiz' to begin.");
            }
            else
            {
                AddBotMessage("Current Question:" + Environment.NewLine + quizManager.GetCurrentQuestion());
            }
            ScrollToBottom();
        }

        private void ProcessQuizAnswer(string answer)
        {
            if (quizManager == null) return;

            if (!isQuizActive)
            {
                AddBotMessage("No quiz is active. Type 'Start quiz' to begin.");
                return;
            }

            bool correct = quizManager.AnswerQuestion(answer);
            string feedback = quizManager.GetLastFeedback();

            if (correct)
            {
                if (activityLogger != null)
                {
                    activityLogger.LogAction("Quiz: Correct answer");
                }

                if (isAudioEnabled && audioPlayer != null)
                {
                    audioPlayer.PlayQuizCorrect();
                }
            }
            else
            {
                if (activityLogger != null)
                {
                    activityLogger.LogAction("Quiz: Incorrect answer - " + feedback);
                }

                if (isAudioEnabled && audioPlayer != null)
                {
                    audioPlayer.PlayQuizIncorrect();
                }
            }

            AddBotMessage((correct ? "Correct!" : "Incorrect") + Environment.NewLine + feedback);

            txtQuizScore.Text = "Score: " + quizManager.Score + "/" + quizManager.TotalQuestions;

            if (quizManager.IsQuizComplete())
            {
                isQuizActive = false;
                string finalMessage = quizManager.GetFinalFeedback();
                AddBotMessage("Quiz Complete!" + Environment.NewLine + finalMessage);

                if (activityLogger != null)
                {
                    activityLogger.LogAction("Quiz completed - Score: " + quizManager.Score + "/" + quizManager.TotalQuestions);
                }

                txtQuizFeedback.Text = finalMessage;

                if (isAudioEnabled && audioPlayer != null)
                {
                    audioPlayer.PlaySuccess();
                }

                ScrollToBottom();
                return;
            }

            AddBotMessage("Next Question:" + Environment.NewLine + quizManager.GetCurrentQuestion());
            ScrollToBottom();
        }

        // Activity Log
        private void ShowActivityLog()
        {
            if (activityLogger == null) return;

            var logs = activityLogger.GetRecentLogs(10);
            if (logs.Count == 0)
            {
                AddBotMessage("No activities logged yet.");
                return;
            }

            string logMessage = "Recent Activity Log:" + Environment.NewLine + Environment.NewLine;
            int count = 1;
            foreach (var log in logs)
            {
                logMessage += count + ". " + log + Environment.NewLine;
                count++;
            }

            AddBotMessage(logMessage);
            ScrollToBottom();
        }

        private void LoadActivityLog()
        {
            if (activityLogger == null) return;

            var logs = activityLogger.GetRecentLogs(8);
            lstActivity.ItemsSource = null;
            lstActivity.ItemsSource = logs;
        }

        private void btnActivity_Click(object sender, RoutedEventArgs e)
        {
            ShowActivityLog();
        }

        private void btnRefreshLog_Click(object sender, RoutedEventArgs e)
        {
            LoadActivityLog();
            AddBotMessage("Activity log refreshed.");
            ScrollToBottom();
        }

        private void btnNewChat_Click(object sender, RoutedEventArgs e)
        {
            ChatStackPanel.Children.Clear();
            ShowWelcomeMessage();

            if (activityLogger != null)
            {
                activityLogger.LogAction("New chat started");
            }
        }

        // Audio Controls
        private void btnAudio_Click(object sender, RoutedEventArgs e)
        {
            if (audioPlayer == null) return;

            isAudioEnabled = !isAudioEnabled;
            audioPlayer.IsAudioEnabled = isAudioEnabled;

            UpdateAudioStatus();

            string status = isAudioEnabled ? "Audio enabled" : "Audio disabled";
            AddBotMessage("Audio: " + status);

            if (activityLogger != null)
            {
                activityLogger.LogAction("Audio toggled: " + status);
            }

            if (isAudioEnabled)
            {
                audioPlayer.PlayNotification();
            }

            ScrollToBottom();
        }

        private void btnStopAudio_Click(object sender, RoutedEventArgs e)
        {
            if (audioPlayer == null) return;

            audioPlayer.StopWav();
            AddBotMessage("Audio stopped.");

            if (activityLogger != null)
            {
                activityLogger.LogAction("Audio stopped by user");
            }

            ScrollToBottom();
        }

        private void btnTestAudio_Click(object sender, RoutedEventArgs e)
        {
            if (audioPlayer == null) return;

            if (isAudioEnabled)
            {
                audioPlayer.PlaySuccess();
                AddBotMessage("Testing audio...");

                if (activityLogger != null)
                {
                    activityLogger.LogAction("Test audio played");
                }

                ScrollToBottom();
            }
            else
            {
                AddBotMessage("Audio is disabled. Enable audio first.");
            }
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (audioPlayer == null) return;

            int volume = (int)e.NewValue;
            audioPlayer.Volume = volume;
            txtVolumeValue.Text = volume + "%";
        }

        private void UpdateAudioStatus()
        {
            if (audioPlayer == null) return;

            if (isAudioEnabled)
            {
                txtAudioStatus.Text = "Audio: Enabled";
                txtAudioStatus.Foreground = System.Windows.Media.Brushes.Green;
                btnAudio.Content = "Disable Audio";
                btnAudio.Background = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtAudioStatus.Text = "Audio: Disabled";
                txtAudioStatus.Foreground = System.Windows.Media.Brushes.Red;
                btnAudio.Content = "Enable Audio";
                btnAudio.Background = System.Windows.Media.Brushes.Green;
            }
        }

        // UI Helpers
        private void AddUserMessage(string message)
        {
            Border userBorder = UIHelper.CreateUserMessage(message);
            ChatStackPanel.Children.Add(userBorder);
            ScrollToBottom();
        }

        private void AddBotMessage(string message)
        {
            Border botBorder = UIHelper.CreateBotMessage(message);
            ChatStackPanel.Children.Add(botBorder);
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToBottom();
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ChatScrollViewer.ScrollToBottom();
            }), DispatcherPriority.Background);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            dbContext?.Dispose();
            audioPlayer?.Dispose();
        }
    }
}