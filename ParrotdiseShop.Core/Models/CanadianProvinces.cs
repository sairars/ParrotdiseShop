using Microsoft.AspNetCore.Mvc.Rendering;

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
