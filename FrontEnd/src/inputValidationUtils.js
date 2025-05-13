const InputValidationUtils = {
  normalizeName: (name) => {
    if (!name || name.trim().length === 0) return name;

    name = name.trim().toLowerCase();
    return name.charAt(0).toUpperCase() + name.slice(1);
  },

  isValidName: (name) => {
    if (!name || name.trim().length < 2) return false;
    return /^[A-Za-zÀ-ÖØ-öø-ÿ]+$/.test(name.trim());
  },

  isValidUsername: (username) => {
    if (!username || username.trim().length < 4) return false;
    return /^[a-zA-Z0-9_]+$/.test(username.trim());
  },

  isValidEmail: (email) => {
    if (!email || email.trim().length === 0) return false;
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email.trim());
  },

  isStrongPassword: (password) => {
    if (!password || password.length < 6) return false;
    return /[A-Z]/.test(password) && // at least one uppercase
           /[a-z]/.test(password) && // at least one lowercase
           /[0-9]/.test(password) && // at least one digit
           /[^A-Za-z0-9]/.test(password); // at least one special character
  }
};

export default InputValidationUtils;
