const PersonalNumberUtils = {
    // Normalize by removing hyphens, spaces, etc.
    normalize: (personalNumber) => {
        return personalNumber?.replace("-", "").replace(" ", "").trim();
    },

    // Format as YYYYMMDD-XXXX (standard Swedish)
    format: (personalNumber) => {
        const normalized = PersonalNumberUtils.normalize(personalNumber);
        if (normalized.length < 12) return normalized;

        return `${normalized.slice(0, 8)}-${normalized.slice(8)}`;
    },

    // Validate Swedish personal number (basic check)
    isValid: (personalNumber) => {
        const norm = PersonalNumberUtils.normalize(personalNumber);
        if (norm.length !== 12 || !/^\d+$/.test(norm)) return false;

        // Extract year, month, and day
        const year = parseInt(norm.slice(0, 4), 10);
        const month = parseInt(norm.slice(4, 6), 10);
        const day = parseInt(norm.slice(6, 8), 10);

        // Check if year is not before 1910
        if (year < 1910) return false;

        // Check if month is between 1 and 12
        if (month < 1 || month > 12) return false;

        // Validate day based on month and leap year
        if (day < 1 || day > PersonalNumberUtils.getMaxDaysInMonth(year, month)) return false;

        return true;
    },

    // Extract birth date from personal number
    extractBirthDate: (personalNumber) => {
        const norm = PersonalNumberUtils.normalize(personalNumber);
        if (norm.length < 8) return null;

        const year = parseInt(norm.slice(0, 4), 10);
        const month = parseInt(norm.slice(4, 6), 10);
        const day = parseInt(norm.slice(6, 8), 10);

        // Check if year is before 1910
        if (year < 1910) return null;

        // Check if month is valid
        if (month < 1 || month > 12) return null;

        // Check if day is valid based on the month and year
        if (day < 1 || day > PersonalNumberUtils.getMaxDaysInMonth(year, month)) return null;

        // Return the date as a Date object
        const dateString = `${year}-${month}-${day}`;
        return new Date(dateString);
    },

    // Get maximum days in month considering leap years
    getMaxDaysInMonth: (year, month) => {
        switch (month) {
            case 1: case 3: case 5: case 7: case 8: case 10: case 12:
                return 31;
            case 4: case 6: case 9: case 11:
                return 30;
            case 2:
                return PersonalNumberUtils.isLeapYear(year) ? 29 : 28;
            default:
                return 0; // Invalid month
        }
    },

    // Leap year check
    isLeapYear: (year) => {
        return (year % 4 === 0 && (year % 100 !== 0 || year % 400 === 0));
    }
};

export default PersonalNumberUtils;
