import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import { store } from './store/store'; // Import Redux store
import { setLanguage } from './languageSlice'; // Import the action
import en from './Eng.json';
import sv from './Sv.json';

const resources = {
  en: { translation: en },
  sv: { translation: sv },
};

i18n.use(initReactI18next).init({
  resources,
  lng: store.getState().language.language, // Get initial language from Redux
  fallbackLng: 'en', // Default language in case the selected language is unavailable
  interpolation: {
    escapeValue: false,
  },
});

// Listen to Redux state changes and update language in i18n
store.subscribe(() => {
  const language = store.getState().language.language;
  i18n.changeLanguage(language);
});

export default i18n;
