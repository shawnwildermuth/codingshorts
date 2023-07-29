import { createI18n } from 'vue-i18n'


import messages from '@intlify/unplugin-vue-i18n/messages'

const i18n = createI18n({
  locale: "en",
  legacy: false,
  messages,
  fallbackLocale: "en"
});

export default i18n;