This file explains how Visual Studio created the project.

The following tools were used to generate this project:
- create-vite

The following steps were used to generate this project:
- Create vue project with create-vite: `npm init --yes vue@latest bakery.apphost.client -- --eslint  --typescript `.
- Update `vite.config.ts` to set up proxying and certs.
- Add `@type/node` for `vite.config.js` typing.
- Update `HelloWorld` component to fetch and display weather information.
- Add `shims-vue.d.ts` for basic types.
- Create project file (`bakery.apphost.client.esproj`).
- Create `launch.json` to enable debugging.
- Create `tasks.json` to enable debugging.
- Add project to solution.
- Update proxy endpoint to be the backend server endpoint.
- Write this file.
