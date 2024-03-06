<script setup lang="ts">
  import { useAuth } from "@/auth";

  const auth = useAuth();

  async function login() {
    await auth.msalInstance.initialize();
    await auth.msalInstance.loginPopup();
    const myAccounts = auth.msalInstance.getAllAccounts();
    auth.account = myAccounts[0];

    const response = await auth.msalInstance.acquireTokenSilent({
      account: auth.account,
      scopes: [`api://${import.meta.env.VITE_CLIENTID}/ourapi`]
    });

    auth.token = response.accessToken;
  }

</script>

<template>
  <div>
    <p>
      <div v-if="auth.account">{{ auth.account.name }}</div>
      <button v-if="!auth.account" @click="login" class="btn">Click here to login</button>
    </p>
  </div>
</template>