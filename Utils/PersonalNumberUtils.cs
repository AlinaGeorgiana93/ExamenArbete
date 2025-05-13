namespace Utils
{
    public static class PersonalNumberUtils
    {
        // Normalize by removing hyphens, spaces, etc.
       public static string Normalize(string personalNumber)
{
    if (string.IsNullOrEmpty(personalNumber))
        throw new ArgumentException("Personal number cannot be null or empty.");

    return personalNumber.Replace("-", "").Replace(" ", "").Trim();
}


        // Format as YYYYMMDD-XXXX (standard Swedish)
            public static string Format(string personalNumber)
            {
                var normalized = Normalize(personalNumber);

                if (string.IsNullOrEmpty(normalized) || normalized.Length != 12)
                    throw new ArgumentException("Personal number must be 12 characters after normalization.");

                return $"{normalized.Substring(0, 8)}-{normalized.Substring(8)}";
            }


        // Validate Swedish personal number (basic check)
         public static bool IsValid(string personalNumber)
{
    var norm = Normalize(personalNumber);

    if (string.IsNullOrEmpty(norm) || norm.Length != 12 || !long.TryParse(norm, out _))
        return false;

    try
    {
        var year = int.Parse(norm.Substring(0, 4));
        var month = int.Parse(norm.Substring(4, 2));
        var day = int.Parse(norm.Substring(6, 2));

        // Check if year is not before 1910 or after the current year
        var currentYear = DateTime.Now.Year;
        if (year < 1910 || year > currentYear)
            return false;

        // Check if month is between 1 and 12
        if (month < 1 || month > 12)
            return false;

        // Validate day based on the month and leap year check
        if (day < 1 || day > GetMaxDaysInMonth(year, month))
            return false;

        return true;
    }
    catch (FormatException)
    {
        // Catch format exceptions if year, month, or day cannot be parsed
        return false;
    }
}



        // Extract birth date
      public static DateTime? ExtractBirthDate(string personalNumber)
{
    var norm = Normalize(personalNumber);
    if (norm.Length < 8)
        throw new ArgumentException("Personal number is too short to extract birth date.");

    try
    {
        var year = int.Parse(norm.Substring(0, 4));
        var month = int.Parse(norm.Substring(4, 2));
        var day = int.Parse(norm.Substring(6, 2));

        // Check if year is before 1910
        if (year < 1910) 
            throw new ArgumentException("Year is invalid (before 1910).");

        // Check if month is valid
        if (month < 1 || month > 12)
            throw new ArgumentException("Month is invalid.");

        // Check if day is valid based on the month and year
        if (day < 1 || day > GetMaxDaysInMonth(year, month))
            throw new ArgumentException("Day is invalid for the given month/year.");

        var dateString = $"{year:D4}-{month:D2}-{day:D2}";
        if (DateTime.TryParse(dateString, out var date))
            return date;

        return null;
    }
    catch (FormatException)
    {
        // Catch format exceptions if year, month, or day cannot be parsed
        return null;
    }
}

        // Get maximum number of days in a month considering leap years
        private static int GetMaxDaysInMonth(int year, int month)
        {
            switch (month)
            {
                case 1:  // January
                case 3:  // March
                case 5:  // May
                case 7:  // July
                case 8:  // August
                case 10: // October
                case 12: // December
                    return 31;

                case 4:  // April
                case 6:  // June
                case 9:  // September
                case 11: // November
                    return 30;

                case 2:  // February
                    // Leap year check: if divisible by 4, but not 100, or divisible by 400
                    return DateTime.IsLeapYear(year) ? 29 : 28;

                default:
                    return 0;  // Invalid month
            }
        }
    }
}
