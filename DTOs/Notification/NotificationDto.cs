using SupplySync.Constants.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupplySync.DTOs.Notification
{
    public class CreateBulkNotificationRequestDto
    {
        public List<int>? UserIDs { get; set; }            // optional
        public List<RoleType>? RoleTypes { get; set; }     // optional

        public int? ContractID { get; set; }

        [Required]
        public string Message { get; set; } = default!;

        [Required]
        public NotificationCategory Category { get; set; }

        public NotificationStatus? Status { get; set; } // defaults to Unread if null
    }

    public class CreateNotificationRequestDto
    {
        [Required]
        public int UserID { get; set; }

        public int? ContractID { get; set; }

        [Required]
        public string Message { get; set; } = default!;

        [Required]
        public NotificationCategory Category { get; set; }

        // Optional: if omitted, service will default to Unread
        public NotificationStatus? Status { get; set; }
    }

    public class NotificationFiltersDto
    {
        public int? UserID { get; set; }
        public int? ContractID { get; set; }
        public NotificationCategory? Category { get; set; }
        public NotificationStatus? Status { get; set; }

        public DateTime? FromUtc { get; set; } // inclusive (uses CreatedDate)
        public DateTime? ToUtc { get; set; }   // inclusive (uses CreatedDate)

        public string? Search { get; set; }    // on Message and User.Name

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public bool Desc { get; set; } = true; // order by CreatedDate
    }

    public class NotificationListResponseDto
    {
        public List<NotificationResponseDto> Items { get; set; } = new();

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }

    public class NotificationResponseDto
    {
        public int NotificationID { get; set; }

        public int UserID { get; set; }
        public string? UserName { get; set; }

        public int? ContractID { get; set; }

        public string Message { get; set; } = default!;
        public NotificationCategory Category { get; set; }
        public NotificationStatus Status { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UpdateNotificationRequestDto
    {
        // For user self update we use only these:
        public NotificationStatus? Status { get; set; } // Read / Archived
        public bool? IsDeleted { get; set; }

        // NOTE: admin update can still use your existing Update route if needed.
    }

}
