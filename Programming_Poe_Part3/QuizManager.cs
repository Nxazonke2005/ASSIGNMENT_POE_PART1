using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot
{
    public class QuizQuestion
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; } = string.Empty;
    }
    // QuizManager class to manage the quiz flow, questions, and scoring

    public class QuizManager
    {
        private List<QuizQuestion> questions = new List<QuizQuestion>();
        private int currentIndex = 0;
        private int score = 0;
        private string lastFeedback = string.Empty;
        private bool quizComplete = false;

        public int TotalQuestions { get; private set; }
        public int Score => score;

        public QuizManager()
        {
            InitializeQuestions();
            TotalQuestions = questions.Count;
        }

        private void InitializeQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Reporting phishing emails helps prevent scams and protects others."
                },
                new QuizQuestion
                {
                    Question = "What is the best practice for creating a strong password?",
                    Options = new List<string> { "Use your birthday", "Use a short password", "Use a mix of characters and length", "Use the same password everywhere" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Strong passwords use uppercase, lowercase, numbers, and symbols for better security."
                },
                new QuizQuestion
                {
                    Question = "What is Two-Factor Authentication (2FA)?",
                    Options = new List<string> { "A password manager", "An extra layer of security", "A type of malware", "A phishing attack" },
                    CorrectAnswerIndex = 1,
                    Explanation = "2FA adds an extra layer of security by requiring a second verification step."
                },
                new QuizQuestion
                {
                    Question = "What is a common sign of a phishing email?",
                    Options = new List<string> { "Professional design", "Urgent language", "Known sender", "Correct spelling" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Phishing emails often create urgency to make you act without thinking."
                },
                new QuizQuestion
                {
                    Question = "What should you do if you receive a suspicious link?",
                    Options = new List<string> { "Click it to see", "Share it with friends", "Hover to check the URL", "Ignore it completely" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Hover over links to see the actual URL before clicking."
                },
                new QuizQuestion
                {
                    Question = "What is malware?",
                    Options = new List<string> { "A type of password", "Malicious software", "A security tool", "A browser extension" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Malware is malicious software designed to damage or gain unauthorized access."
                },
                new QuizQuestion
                {
                    Question = "How often should you update your passwords?",
                    Options = new List<string> { "Never", "Every 10 years", "Every 3-6 months", "Only when forced" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Regular password updates help protect against unauthorized access."
                },
                new QuizQuestion
                {
                    Question = "What does 'https://' indicate?",
                    Options = new List<string> { "The website is secure", "The website is slow", "The website is new", "The website is popular" },
                    CorrectAnswerIndex = 0,
                    Explanation = "'https://' indicates a secure connection with data encryption."
                },
                new QuizQuestion
                {
                    Question = "What is social engineering?",
                    Options = new List<string> { "Building social networks", "Manipulating people for information", "Creating fake profiles", "Online shopping" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Social engineering manipulates people into revealing confidential information."
                },
                new QuizQuestion
                {
                    Question = "What should you do with sensitive data?",
                    Options = new List<string> { "Share it online", "Encrypt and secure it", "Post it on social media", "Send it via email" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Sensitive data should always be encrypted and properly secured."
                },
                new QuizQuestion
                {
                    Question = "What is a VPN used for?",
                    Options = new List<string> { "To speed up internet", "To secure internet connection", "To download files", "To watch videos" },
                    CorrectAnswerIndex = 1,
                    Explanation = "A VPN secures your internet connection and protects your privacy."
                },
                new QuizQuestion
                {
                    Question = "What is ransomware?",
                    Options = new List<string> { "A type of antivirus", "Malware that encrypts files", "A password manager", "A browser" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Ransomware encrypts files and demands payment for decryption."
                }
            };
        }

        public void StartQuiz()
        {
            currentIndex = 0;
            score = 0;
            quizComplete = false;
            lastFeedback = string.Empty;
        }

        public string GetCurrentQuestion()
        {
            if (currentIndex >= questions.Count)
            {
                quizComplete = true;
                return "Quiz completed!";
            }

            var q = questions[currentIndex];
            string questionText = q.Question + Environment.NewLine + Environment.NewLine;

            char optionLetter = 'A';
            foreach (var option in q.Options)
            {
                questionText += optionLetter + ") " + option + Environment.NewLine;
                optionLetter++;
            }

            return questionText;
        }
        // Method to answer the current question and provide feedback

        public bool AnswerQuestion(string answer)
        {
            if (currentIndex >= questions.Count)
            {
                quizComplete = true;
                lastFeedback = "Quiz already completed.";
                return false;
            }

            var q = questions[currentIndex];
            int selectedIndex = -1;

            answer = answer.ToUpper().Trim();
            if (answer.Length == 1 && answer[0] >= 'A' && answer[0] <= 'D')
            {
                selectedIndex = answer[0] - 'A';
            }
            else if (answer.Length > 1)
            {
                for (int i = 0; i < q.Options.Count; i++)
                {
                    if (q.Options[i].ToLower().Contains(answer.ToLower()) || answer.Contains(q.Options[i].ToLower()))
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }
            // If the answer is not valid, return false

            bool correct = (selectedIndex == q.CorrectAnswerIndex);
            lastFeedback = q.Explanation;

            if (correct)
                score++;

            currentIndex++;
            return correct;
        }

        public string GetLastFeedback()
        {
            return lastFeedback;
        }
        // Method to check if the quiz is complete

        public bool IsQuizComplete()
        {
            return currentIndex >= questions.Count || quizComplete;
        }
        // Method to get final feedback based on the score

        public string GetFinalFeedback()
        {
            double percentage = (double)score / TotalQuestions * 100;

            if (percentage >= 80)
                return "Excellent! Score: " + score + "/" + TotalQuestions + " (" + percentage + "%) - You're a cybersecurity pro!";
            else if (percentage >= 60)
                return "Good job! Score: " + score + "/" + TotalQuestions + " (" + percentage + "%) - Keep learning to improve!";
            else
                return "Keep practicing! Score: " + score + "/" + TotalQuestions + " (" + percentage + "%) - Review the topics to improve.";
        }
    }
}