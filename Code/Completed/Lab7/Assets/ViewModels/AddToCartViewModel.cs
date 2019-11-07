﻿#region Copyright
// Copyright Information
// ==================================
// SpyStore.Hol - SpyStore.Hol.Mvc - AddToCartViewModel.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 02/24/2019
// See License.txt for more information
// ==================================
#endregion

using System.ComponentModel.DataAnnotations;
using SpyStore.Hol.Models.ViewModels;

namespace SpyStore.Hol.Mvc.Models.ViewModels
{
    public class AddToCartViewModel : CartRecordWithProductInfo
    {
        [Required]
        public new int Quantity { get; set; }
    }
}