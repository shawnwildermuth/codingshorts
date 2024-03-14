<script setup lang="ts">
import type { Project, Customer } from '@/models';
import { onMounted, reactive, ref } from 'vue';

const items = reactive(Array<Customer>());
const item = ref<Customer>();
const theDialog = ref<HTMLDialogElement>();
const projects = reactive(new Array<Project>())
const showSaved = ref(false);

onMounted(async () => {
  const result = await fetch("https://restdesign.dev/api/customers");
  if (result.ok) {
    const data = await result.json();
    items.splice(0, items.length, ...data);
  }
});

async function show(customer: Customer) {
  item.value = customer;

  const result = await fetch(`https://restdesign.dev/api/customers/${customer.id}/projects`);
  if (result.ok) {
    const data = await result.json();
    projects.splice(0, projects.length, ...data);
  }
}

function formatCity(customer: Customer) {
  return `${customer.city}, ${customer.stateProvince}  ${customer.postalCode}`;
}

function showModal() {
  theDialog.value?.showModal();
}

function save() {
  theDialog.value?.close();
  showSaved.value = true;
  setTimeout(() => {
    showSaved.value = false;
  }, 2000)
}

function toDate(value: string | null) {
  const formatter = new Intl.DateTimeFormat("en-us");

  if (value) return formatter.format(new Date(value));
}

</script>

<template>
  <main>
    <div class="flex justify-between pb-1">
      <h3 class="font-bold text-lg">Customers</h3>
      <button class="btn btn-primary btn-sm" @click="showModal">New
        Customer</button>
    </div>
    <div class="flex min-h-screen">
      <ul class="menu p-1 pl-0">
        <li v-for="item in items">
          <a href="#" @click.prevent="show(item)">
            {{ item.companyName }}
          </a>
        </li>
      </ul>
      <div class="border p-2 w-full">
        <div v-if="item">
          <h3 class="font-bold text-lg">{{ item.companyName }}</h3>
          <div>
            <div>{{ item.contact }}</div>
            <div>{{ item.phoneNumber }}</div>
            <div>{{ item.addressLine1 }}</div>
            <div>{{ item.addressLine2 }}</div>
            <div>{{ item.addressLine3 }}</div>
            <div>{{ formatCity(item) }}</div>
            <div>{{ item.country }}</div>
          </div>
          <strong>Projects</strong>
          <table class="table table-auto">
            <thead>
              <tr>
                <th>Name</th>
                <th>Start</th>
                <th>End</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="p in projects">
                <td>{{ p.projectName }}</td>
                <td>{{ toDate(p.startDate) }}</td>
                <td>{{ toDate(p.endDate) }}</td>
                <td>
                  <div class="join">
                    <button
                      class="btn btn-xs btn-accent join-item">edit</button>
                    <button
                      class="btn btn-xs btn-warning join-item">delete</button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <div class="toast" v-if="showSaved">
      <div class="alert alert-success">
        Saved...
      </div>
    </div>
    <dialog ref="theDialog" class="modal backdrop:backdrop-blur-sm">
      <div class="modal-box flex flex-col p-1">
        <label>Company Name</label>
        <input />
        <label>Contact</label>
        <input />
        <label>Phone</label>
        <input />
        <label>Address</label>
        <input />
        <input />
        <input />
        <label>City</label>
        <input />
        <label>State</label>
        <input />
        <label>Zip</label>
        <input />
        <label>Country</label>
        <select>
          <option disabled selected>Choose One...</option>
          <option>USA</option>
          <option>Canada</option>
          <option>UK</option>
          <option>France</option>
          <option>Germany</option>
          <option>Mexico</option>
        </select>
        <div class="modal-action flex justify-end gap-1">
          <button class="btn btn-info"
            @click="theDialog?.close()">Cancel</button>
          <button class="btn btn-success" @click="save">Save</button>
        </div>
      </div>
    </dialog>
  </main>
</template>

<style scoped>
input {
  @apply input input-bordered;
}

select {
  @apply select select-bordered;
}

label {
  @apply label label-text;
}
</style>
