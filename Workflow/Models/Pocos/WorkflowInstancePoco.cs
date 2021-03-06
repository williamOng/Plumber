﻿using System;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Workflow.Extensions;
using Workflow.Helpers;

namespace Workflow.Models
{
    [TableName("WorkflowInstance")]
    [ExplicitColumns]
    [PrimaryKey("Id", autoIncrement = true)]
    public class WorkflowInstancePoco
    {
        private IPublishedContent _node;
        private IUser _authorUser;

        private readonly Utility _utility;

        public WorkflowInstancePoco()
        {
            TaskInstances = new HashSet<WorkflowTaskPoco>();
            Status = (int)WorkflowStatus.PendingApproval;
            CreatedDate = DateTime.Now;
            CompletedDate = null;

            _utility = new Utility();
        }

        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("Guid")]
        public Guid Guid { get; set; }

        [Column("NodeId")]
        public int NodeId { get; set; }

        [Column("Type")]
        public int Type { get; set; }

        [Column("TotalSteps")]
        public int TotalSteps { get; set; }

        [Column("AuthorUserId")]
        public int AuthorUserId { get; set; }

        [Column("Status")]
        public int Status { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("CompletedDate")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime? CompletedDate { get; set; }

        [Column("AuthorComment")]
        public string AuthorComment { get; set; }

        [ResultColumn]
        public WorkflowStatus WorkflowStatus => (WorkflowStatus)Status;

        [ResultColumn]
        public WorkflowType WorkflowType => (WorkflowType)Type;

        /// <summary>
        /// The document object associated with this workflow.
        /// </summary>
        [ResultColumn]
        public IPublishedContent Node => _node ?? (_node = _utility.GetPublishedContent(NodeId));

        /// <summary>
        /// The author user who initiated this workflow instance.
        /// </summary>
        [ResultColumn]
        public IUser AuthorUser => _authorUser ?? (_authorUser = _utility.GetUser(AuthorUserId));

        /// <summary>
        /// Title case text name for the workflow status.
        /// </summary>
        [ResultColumn]
        public string StatusName => WorkflowStatus.ToString().ToTitleCase();

        /// <summary>
        /// Indicates whether the workflow instance is currently active.
        /// </summary>
        [ResultColumn]
        public bool Active => WorkflowStatus.NotIn(WorkflowStatus.Cancelled, WorkflowStatus.Errored, WorkflowStatus.Approved);

        [ResultColumn]
        public DateTime? ScheduledDate { get; set; }

        [ResultColumn]
        public ICollection<WorkflowTaskPoco> TaskInstances { get; set; }

    }
}
