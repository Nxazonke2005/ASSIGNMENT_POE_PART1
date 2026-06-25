using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CyberSecurityAwarenessBot
{
    public partial class MainWindow : Window
    {
        private Chatbot chatbot;
        private DatabaseHelper dbHelper;
        private QuizManager quizManager;
        private ActivityLogger activityLogger;
        private List<TaskItem> currentTasks;
        private bool isQuizActive = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComponents();
            ShowWelcomeMessage();
            LoadTasks();
            LoadActivityLog();
            txtMessage.Focus();
        }

        private void InitializeComponents()
        {
            chatbot = new Chatbot();
            dbHelper = new DatabaseHelper();
            quizManager = new QuizManager();
            activityLogger = ActivityLogger.Instance;
            activityLogger.LogAction("Application started - Part 3");
        }

        private void ShowWelcomeMessage()
        {
            string welcome = "Hello. I'm your Cybersecurity Assistant - Part 3." + Environment.NewLine + Environment.NewLine +
                "I can help you with:" + Environment.NewLine +
                "- Cybersecurity topics (phishing, passwords, malware)" + Environment.NewLine +
                "- Task management with reminders" + Environment.NewLine +
                "- Cybersecurity quizzes" + Environment.NewLine +
                "- Activity tracking" + Environment.NewLine + Environment.NewLine +
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

            if (isQuizActive)
            {
                ProcessQuizAnswer(userInput);
                return;
            }

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
        }

        // Task Management
        private void HandleTaskAddition(string input)
        {
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

            TaskItem task = new TaskItem
            {
                Title = taskTitle,
                Description = taskDescription,
                Reminder = reminder,
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };

            dbHelper.AddTask(task);
            activityLogger.LogAction("Task added: " + taskTitle + (reminder != null ? " (" + reminder + ")" : ""));

            LoadTasks();

            string response = "Task added: " + taskTitle + Environment.NewLine +
                "Description: " + taskDescription + Environment.NewLine +
                (reminder != null ? "Reminder: " + reminder : "No reminder set");

            AddBotMessage(response);
            ScrollToBottom();
        }

        private void LoadTasks()
        {
            try
            {
                currentTasks = dbHelper.GetAllTasks();
                lstTasks.ItemsSource = null;
                lstTasks.ItemsSource = currentTasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading tasks: " + ex.Message);
            }
        }

        private void btnCompleteTask_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            TaskItem task = btn.Tag as TaskItem;
            if (task != null)
            {
                task.IsCompleted = true;
                dbHelper.UpdateTask(task);
                activityLogger.LogAction("Task completed: " + task.Title);
                LoadTasks();
                AddBotMessage("Task " + task.Title + " marked as completed.");
                ScrollToBottom();
            }
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            TaskItem task = btn.Tag as TaskItem;
            if (task != null)
            {
                dbHelper.DeleteTask(task.Id);
                activityLogger.LogAction("Task deleted: '" + task.Title + "'");
                LoadTasks();
                AddBotMessage("Task '" + task.Title + "' deleted.");
                ScrollToBottom();
            }
        }

        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            AddBotMessage("Your Tasks:" + Environment.NewLine + GetTaskList());
            ScrollToBottom();
        }

        private string GetTaskList()
        {
            if (currentTasks == null || currentTasks.Count == 0)
                return "No tasks found. Add a task like: 'Add task to enable 2FA'";

            string result = "";
            int count = 1;
            foreach (var task in currentTasks)
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
            if (isQuizActive)
            {
                AddBotMessage("A quiz is already in progress. Answer the current question.");
                return;
            }

            isQuizActive = true;
            quizManager.StartQuiz();

            activityLogger.LogAction("Quiz started");

            AddBotMessage("Starting Cybersecurity Quiz!" + Environment.NewLine + Environment.NewLine + quizManager.GetCurrentQuestion());
            ScrollToBottom();
            txtQuizScore.Text = "Score: 0/" + quizManager.TotalQuestions;
            txtQuizFeedback.Text = "Answer the question in the chat.";
        }

        private void btnQuiz_Click(object sender, RoutedEventArgs e)
        {
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
            if (!isQuizActive)
            {
                AddBotMessage("No quiz is active. Type 'Start quiz' to begin.");
                return;
            }

            bool correct = quizManager.AnswerQuestion(answer);
            string feedback = quizManager.GetLastFeedback();

            if (correct)
            {
                activityLogger.LogAction("Quiz: Correct answer");
            }
            else
            {
                activityLogger.LogAction("Quiz: Incorrect answer - " + feedback);
            }

            AddBotMessage((correct ? "Correct!" : "Incorrect") + Environment.NewLine + feedback);

            txtQuizScore.Text = "Score: " + quizManager.Score + "/" + quizManager.TotalQuestions;

            if (quizManager.IsQuizComplete())
            {
                isQuizActive = false;
                string finalMessage = quizManager.GetFinalFeedback();
                AddBotMessage("Quiz Complete!" + Environment.NewLine + finalMessage);
                activityLogger.LogAction("Quiz completed - Score: " + quizManager.Score + "/" + quizManager.TotalQuestions);
                txtQuizFeedback.Text = finalMessage;
                ScrollToBottom();
                return;
            }

            AddBotMessage("Next Question:" + Environment.NewLine + quizManager.GetCurrentQuestion());
            ScrollToBottom();
        }

        // Activity Log
        private void ShowActivityLog()
        {
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
            activityLogger.LogAction("New chat started");
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
    }
}