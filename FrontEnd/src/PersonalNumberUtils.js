const PersonalNumberUtils = {
  // Normalize by removing hyphens, spaces, etc.
  normalize: (personalNumber) => {
    return personalNumber?.replace(/[-\s]/g, '').trim() || '';
  },

  // Format as YYYYMMDD-XXXX or with last 4 digits masked
  format: (personalNumber, maskLastFour = false) => {
    const normalized = PersonalNumberUtils.normalize(personalNumber);
    if (normalized.length < 12) return normalized;

    const firstPart = normalized.slice(0, 8);
    const lastPart = maskLastFour ? '****' : normalized.slice(8);
    return `${firstPart}-${lastPart}`;
  },

  // Validate Swedish personal number (basic check)
  isValid: (personalNumber) => {
    const norm = PersonalNumberUtils.normalize(personalNumber);
    if (!/^\d{10}$|^\d{12}$/.test(norm)) return false;

    let year, month, day;
    if (norm.length === 12) {
      year = parseInt(norm.slice(0, 4), 10);
      month = parseInt(norm.slice(4, 6), 10);
      day = parseInt(norm.slice(6, 8), 10);
    } else {
      // Guess century: assume 1900s for now, or adjust with heuristics if needed
      const shortYear = parseInt(norm.slice(0, 2), 10);
      year = shortYear >= 10 ? 1900 + shortYear : 2000 + shortYear;
      month = parseInt(norm.slice(2, 4), 10);
      day = parseInt(norm.slice(4, 6), 10);
    }

    // Add condition to validate the year (cannot be below 1910)
    if (year < 1910 || year > new Date().getFullYear()) return false;

    if (month < 1 || month > 12) return false;

    const maxDays = PersonalNumberUtils.getMaxDaysInMonth(year, month);
    return day >= 1 && day <= maxDays;
  },
    mask(value) {
    const normalized = value.replace(/\D/g, '');
    if (normalized.length < 4) return '****';
    const visible = normalized.slice(0, -4);
    return visible + '****';
  },

  // Extract birth date as a JavaScript Date object
  extractBirthDate: (personalNumber) => {
    const norm = PersonalNumberUtils.normalize(personalNumber);
    if (norm.length < 8) return null;

    const year = parseInt(norm.slice(0, 4), 10);
    const month = parseInt(norm.slice(4, 6), 10);
    const day = parseInt(norm.slice(6, 8), 10);

    // Validate extracted year, month, and day
    if (
      year < 1910 ||
      month < 1 || month > 12 ||
      day < 1 || day > PersonalNumberUtils.getMaxDaysInMonth(year, month)
    ) {
      return null;
    }

    try {
      return new Date(`${year}-${String(month).padStart(2, '0')}-${String(day).padStart(2, '0')}`);
    } catch {
      return null;
    }
  },

  // Get maximum number of days in a month considering leap years
  getMaxDaysInMonth: (year, month) => {
    switch (month) {
      case 1: case 3: case 5: case 7: case 8: case 10: case 12:
        return 31;
      case 4: case 6: case 9: case 11:
        return 30;
      case 2:
        return (year % 4 === 0 && (year % 100 !== 0 || year % 400 === 0)) ? 29 : 28;
      default:
        return 0;
    }
  }
};

export default PersonalNumberUtils;
