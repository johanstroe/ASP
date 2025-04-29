namespace ASP_Presentation.Helpers
{
        public static class ProjectHelpers
        {
            public static (string TimeLeftDisplay, string TimeStatus) GetTimeDisplay(DateTime? endDate)
            {
                var end = endDate ?? DateTime.Now;
                var totalDaysLeft = (int)Math.Ceiling((end - DateTime.Now).TotalDays);

                string timeLeftDisplay;
            if (totalDaysLeft < 0)
                timeLeftDisplay = "Expired";
            else if (totalDaysLeft <= 1)
                timeLeftDisplay = "Last day";
            else if (totalDaysLeft < 7)
                timeLeftDisplay = $"{totalDaysLeft} day{(totalDaysLeft == 1 ? "" : "s")} left";
            else if (totalDaysLeft < 30)
                timeLeftDisplay = $"{(int)Math.Ceiling(totalDaysLeft / 7.0)} week{(Math.Ceiling(totalDaysLeft / 7.0) == 1 ? "" : "s")} left";
            else
                timeLeftDisplay = $"{(int)Math.Ceiling(totalDaysLeft / 30.0)} month{(Math.Ceiling(totalDaysLeft / 30.0) == 1 ? "" : "s")} left";

                string timeStatus = totalDaysLeft <= 0
                    ? "expired"
                    : totalDaysLeft < 14
                        ? "urgent"
                        : "on-track";

                return (timeLeftDisplay, timeStatus);
            }

            public static string GetStatusKey(int statusId)
            {
                return statusId switch
                {
                    1 => "ongoing",
                    2 => "notstarted",
                    3 => "completed",
                    _ => "unknown"
                };
            }
        }
    }

