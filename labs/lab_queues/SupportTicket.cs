using System;

namespace QueueLab
{
    /// <summary>
    /// Represents a support ticket in our IT Support Desk Queue system.
    /// This class encapsulates all the information about a support request.
    /// </summary>
    public class SupportTicket
    {
        public string TicketId { get; set; } = "";
        public string Description { get; set; } = "";
        public string Priority { get; set; } = "Normal";
        public DateTime SubmittedAt { get; set; }
        public string SubmittedBy { get; set; } = "User";

        /// <summary>
        /// Creates a new support ticket with the specified details.
        /// </summary>
        /// <param name="ticketId">Unique identifier for the ticket</param>
        /// <param name="description">Description of the issue</param>
        /// <param name="priority">Priority level (Normal, High, Urgent)</param>
        /// <param name="submittedBy">Name of person submitting the ticket</param>
        public SupportTicket(string ticketId, string description, string priority = "Normal", string submittedBy = "User")
        {
            TicketId = ticketId;
            Description = description;
            Priority = priority;
            SubmittedBy = submittedBy;
            SubmittedAt = DateTime.Now;
        }

        /// <summary>
        /// Calculates how long this ticket has been waiting in the queue.
        /// </summary>
        /// <returns>TimeSpan representing wait time</returns>
        public TimeSpan GetWaitTime()
        {
            return DateTime.Now - SubmittedAt;
        }

        /// <summary>
        /// Formats the wait time in a user-friendly way.
        /// </summary>
        /// <returns>String representation of wait time</returns>
        public string GetFormattedWaitTime()
        {
            var waitTime = GetWaitTime();
            if (waitTime.TotalDays >= 1)
                return $"{waitTime.Days}d {waitTime.Hours}h {waitTime.Minutes}m";
            else if (waitTime.TotalHours >= 1)
                return $"{waitTime.Hours}h {waitTime.Minutes}m";
            else if (waitTime.TotalMinutes >= 1)
                return $"{waitTime.Minutes}m {waitTime.Seconds}s";
            else
                return $"{waitTime.Seconds}s";
        }

        /// <summary>
        /// Gets the priority emoji for visual display.
        /// </summary>
        /// <returns>Emoji representing priority level</returns>
        public string GetPriorityEmoji()
        {
            return Priority.ToLower() switch
            {
                "urgent" => "🔴",
                "high" => "🟡",
                "normal" => "🟢",
                _ => "⚪"
            };
        }

        /// <summary>
        /// Creates a formatted display string for the ticket.
        /// </summary>
        /// <returns>Formatted string for display purposes</returns>
        public override string ToString()
        {
            return $"{GetPriorityEmoji()} {TicketId} - {Description} (Wait: {GetFormattedWaitTime()})";
        }

        /// <summary>
        /// Creates a detailed display string for the ticket.
        /// </summary>
        /// <returns>Detailed formatted string</returns>
        public string ToDetailedString()
        {
            return $"""
                   Ticket ID: {TicketId}
                   Description: {Description}
                   Priority: {GetPriorityEmoji()} {Priority}
                   Submitted By: {SubmittedBy}
                   Submitted At: {SubmittedAt:yyyy-MM-dd HH:mm:ss}
                   Wait Time: {GetFormattedWaitTime()}
                   """;
        }
    }
}