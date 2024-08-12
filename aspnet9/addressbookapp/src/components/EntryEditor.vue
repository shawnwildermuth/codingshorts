<script setup lang="ts">
import type { EntryModel } from '@/models';
import { useStore } from '@/store';
import { onMounted, ref } from 'vue';
import { cloneDeep } from "lodash";
import { useRouter } from 'vue-router';
import { entrySchema } from "@/schemas/bookEntry";
import { type inferFormattedError } from "zod";
import ValidationError from './ValidationError.vue';
import DatePicker from "vue-tailwind-datepicker";

const store = useStore();

const router = useRouter();

const props = defineProps<{
  id: number
}>();

const entry = ref<EntryModel>();
const isDirty = ref(false);

type ErrorsType = inferFormattedError<typeof entrySchema>;

const errors = ref<ErrorsType>();

onMounted(async () => {
  if (props.id === -1) {// new
    entry.value = {
      id: 0,
      firstName: "Shawn",
      middleName: "",
      lastName: "Wildermuth",
      email: "shawn@aol.com",
      gender: "Pick One...",
      dateOfBirth: "",
      cellPhone: "",
      homePhone: "",
      workPhone: "",
      companyName: "",
      addresses: [],
    };
  } else {
    const original = await store.getEntryById(props.id);
    if (!original) router.push("/"); 
    const copy = cloneDeep(original) as EntryModel;
    entry.value = copy;
  }
});

function onChange() {
  isDirty.value = true;
}

function cancel() {
  if (props.id > 0) {
  router.push(`/details/${props.id}`);
  } else {
    router.push("/");
  }
}

async function save() {
  const result = entrySchema.safeParse(entry.value);
  if (result.success) {
    let id = props.id;
    let success = false;
    // If New
    if (props.id === -1) {
      const newId = await store.saveEntry(entry.value!);
      if (newId) id = newId;
      success = true;
    } else {
      // update
      if (await store.updateEntry(entry.value!)) {
        success = true;
      } else {
        store.errorMessage = "Failed to update entry.";
      } 
    }
    if (success) {
      router.push({ name: "entry", params: { id: id.toString() } });
    }
  } else {
    errors.value = result.error.format();
  }
}

</script>
<template>
  <div class="flex flex-col w-full lg:w-4/5 mx-auto rounded text-lg p-2 shadow-lg my-4 bg-neutral" v-if="entry">
    <h2 class="text-2xl font-bold mb-2">Edit Address</h2>
    <div class="label">
      <span class="label-text font-bold">
      Name
      </span>
    </div>
    <div class="flex justify-stretch gap-1 flex-col md:flex-row">
      <div class="flex flex-col lg:w-2/5">
        <input class="input input-bordered input-primary" placeholder="first name" v-model="entry.firstName" @input="onChange" />
        <ValidationError :errors="errors" fieldName="firstName" />
      </div>
      <div class="flex flex-col lg:w-1/5">
        <input class="input" placeholder="middle name"
          v-model="entry.middleName" @input="onChange"  />
        <ValidationError :errors="errors" fieldName="middleName" />
      </div>
      <div class="flex flex-col lg:w-2/5">
        <input class="input" placeholder="last name" v-model="entry.lastName" @input="onChange"  />
        <ValidationError :errors="errors" fieldName="lastName" />
      </div>
    </div>
    <div class="label">
      <span class="label-text font-bold">
        Email      
      </span>
    </div>
    <input class="input" placeholder="e.g. you@us.net" v-model="entry.email" @input="onChange"  />
    <ValidationError :errors="errors" fieldName="email" />

    <div class="label">
      <span class="label-text font-bold">
        Company Name
      </span>
    </div>
    <input class="input" placeholder="e.g. My Company, LLC" v-model="entry.companyName" @input="onChange"  />
    <ValidationError :errors="errors" fieldName="companyName" />

    <div class="label">
      <span class="label-text font-bold">
        Home Phone
      </span>
    </div>
    <input class="input" placeholder="e.g. +1 404 227 3030" v-model="entry.homePhone" @input="onChange"  />
    <ValidationError :errors="errors" fieldName="homePhone" />

    <div class="label">
      <span class="label-text font-bold">
        Work Phone
      </span> 
    </div>
    <input class="input" placeholder="e.g. +1 404 227 3030" v-model="entry.workPhone" @input="onChange"  />
    <ValidationError :errors="errors" fieldName="workPhone" />

    <div class="label">
      <span class="label-text font-bold">
        Cell Phone
      </span>
    </div>
    <input class="input" placeholder="e.g. +1 404 227 3030" v-model="entry.cellPhone" @input="onChange"  />
    <ValidationError :errors="errors" fieldName="cellPhone" />

    <div class="label">
      <span class="label-text font-bold">
        Gender
      </span>
    </div>
    <select class="select" v-model="entry.gender" @input="onChange"  >
      <option disabled selected value="">Select One...</option>
      <option>Male</option>
      <option>Female</option>
      <option>Non-binary</option>
      <option>Intersex</option>
      <option>Other</option>
    </select>
    <ValidationError :errors="errors" fieldName="gender" />

    <div class="label">
      <span class="label-text font-bold">
        Birthdate
      </span>
    </div>

    <date-picker class="dark" input-classes="input" as-single placeholder="2000-01-01" v-model="entry.dateOfBirth" @input="onChange"  />
    <ValidationError :errors="errors" fieldName="dateOfBirth" />

    <div class="flex justify-end mt-2">
      <button class="btn"
        @click="cancel">Cancel</button>
      <button class="btn btn-primary" @click="save" :disabled="!isDirty">Save</button>
    </div>
  </div>
</template>