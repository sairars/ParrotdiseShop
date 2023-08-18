using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParrotdiseShop.Core.Models
{
	public class CanadianProvinces
	{
		public static readonly IDictionary<string, string> ProvinceDictionary = 
			new Dictionary<string, string> 
			{
				{"AB", "Alberta"},
				{"BC", "British Columbia"},
				{"MB", "Manitoba"},
				{"NB" , "New Brunswick"},
				{"NL" , "Newfoundland And Labrador"},
				{"NT" , "Northwest Territories"},
				{"NS" , "Nova Scotia"},
				{"NU" , "Nunavut"},
				{"ON" , "Ontario"},
				{"PE" , "Prince Edward Island"},
				{"QC" , "Quebec"},
				{"SK" , "Saskatchewan"},
				{"YT" , "Yukon" }
			};

		public static IEnumerable<SelectListItem> Provinces
		{
			get
			{
				return ProvinceDictionary
						.Select(p => new SelectListItem
						{
							Text = p.Value,
							Value = p.Key
						});
			}
		}
	}
}
