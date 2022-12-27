import { reactive } from "vue";

export default reactive({
  addItem(item) {
    item.id = Math.max(...this.items.map(i => i.id)) + 1;
    this.items.push(item);
  },
  findItem(id) {
    return this.items.find(i => i.id === Number(id));
  },
  items: [
    { id: 1, name: "Bob", age: "53" },
    { id: 2, name: "Jake", age: "66" },
    { id: 3, name: "Sue", age: "42" },
    { id: 4, name: "Sally", age: "36" },
    { id: 5, name: "Phil", age: "25" },
  ],
});
