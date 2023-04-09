﻿using ship_convenient.Model.ReportModel;

namespace ship_convenient.Entities
{
    public class Report : BaseEntity
    {
        public string TypeOfReport { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        #region Relationship
        public Guid PackageId { get; set; }
        public Package? Package { get; set; }
        public Guid AccountId { get; set; }
        public Account? Account { get; set; }
        #endregion

        public ResponseReportModel ToResponseModel() {
            return new ResponseReportModel()
            {
                Id = Id,
                TypeOfReport = TypeOfReport,
                Reason = Reason,
                Status = Status,
                Note = Note,
                Result = Result,
                CreatedAt = CreatedAt,
                ModifiedAt = ModifiedAt,
                PackageId = PackageId,
                Package = Package?.ToResponseModel(),
                AccountId = AccountId,
                Account = Account?.ToResponseModel(),
            };
        }
    }
}
