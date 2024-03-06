import { PublicClientApplication, type AccountInfo } from "@azure/msal-browser";
import { reactive } from "vue";

const config = {
  auth: {
    clientId: import.meta.env.VITE_CLIENTID,
    authority: "https://login.microsoftonline.com/" + import.meta.env.VITE_TENANTID
  }
};

const data = reactive({
  account: null as AccountInfo | null,
  msalInstance: new PublicClientApplication(config),
  token: ""
});

export function useAuth() {
  return data;
}