using System.Collections.Generic;
using VersionOriginalApiService.Domain.Dtos;

namespace VersionOriginalApp.Domain.Dtos
{
    public class RentSummaryDto
    {
        public int TotalRentsOfFilms { get; set; }
        public int TotalRentsOfGames { get; set; }
        public IEnumerable<YearAmountDto> ExpensesByYear { get; set; }
        public IEnumerable<YearAmountDto> FilmsExpensesByYear { get; set; }
        public IEnumerable<YearAmountDto> GamesExpensesByYear { get; set; }
        public IEnumerable<YearAmountDto> RevenuesByYear { get; set; }
        public IEnumerable<YearAmountDto> FilmsRevenuesByYear { get; set; }
        public IEnumerable<YearAmountDto> GamesRevenuesByYear { get; set; }
        public IEnumerable<YearAmountDto> FilmsRentsRevenuesAverageByYear { get; set; }
        public IEnumerable<YearAmountDto> GamesRentsRevenuesAverageByYear { get; set; }
    }
}
