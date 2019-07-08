﻿using System;
using System.ComponentModel.DataAnnotations;
using DFC.App.Help.DataAnnotations;
using Newtonsoft.Json;

namespace DFC.App.Help.Models.Cosmos
{
    public class HelpPageModel
    {
        [Guid]
        [JsonProperty(PropertyName = "id")]
        public Guid DocumentId { get; set; }

        [Required]
        [LowerCase]
        [UrlPath]
        public string CanonicalName { get; set; }

        [Display(Name = "Breadcrumb Title")]
        public string BreadcrumbTitle { get; set; }

        [Display(Name = "Include In SiteMap")]
        public bool IncludeInSitemap { get; set; }

        public MetaTagsModel MetaTags { get; set; }

        public string Content { get; set; }

        [Display(Name = "Last Reviewed")]
        public DateTime LastReviewed { get; set; }

        [UrlPath]
        [LowerCase]
        public string[] AlternativeNames { get; set; }
    }
}
