<template xmlns:v-bind="http://www.w3.org/1999/xhtml" xmlns:v-on="http://www.w3.org/1999/xhtml">
  <h1>Tt</h1>
  <div>
    <!-- input field for filtering the table -->
    <input v-model="filterText" placeholder="Filter records">

    <!-- table to display the data -->
    <table>
      <thead>
      <tr>
        <th>Id</th>
        <th>Date</th>
        <th>Value</th>
        <th>SensorId</th>
        <th>Type</th>
        <th>Unit</th>
      </tr>
      </thead>
      <tbody>
      <!-- iterate over the data and display it in the table -->
      <tr v-for="(measurement, index) in filteredMeasurements" v-bind:key="index">
        <td>{{ measurement.id }}</td>
        <td>{{ measurement.date }}</td>
        <td>{{ measurement.value }}</td>
        <td>{{ measurement.sensorId }}</td>
        <td>{{ measurement.type }}</td>
        <td>{{ measurement.unit }}</td>
      </tr>
      </tbody>
    </table>
    <button type="button" v-on:click="saveFile()">export as JSON</button>
    <input v-model="filterText" placeholder="enter sensor id">
    <div>
      <chart :options="chartOptionsBar"></chart>
    </div>

  </div>
</template>

<script>
import axios from "axios";
import 'echarts/lib/chart/line';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
export default {
  name: "Tables",
  data: function () {
    return {
      measurements: [], // initialize empty array for storing the data
      filterText: '', // initialize empty string for storing the filter text
      chartOptionsBar: {
        xAxis: {
          data: ['Q1', 'Q2', 'Q3', 'Q4']
        },
        yAxis: {
          type: 'value'
        },
        series: [
          {
            type: 'line',
            data: [63, 75, 24, 92]
          }
        ]
      }
    };
  },
  mounted: function () {
    this.getMeasurements()
    console.log('mounted: got here')
  },
  computed: {
    // computed property to filter the data based on the filter text
    filteredMeasurements() {
      return this.measurements.filter(measurement => {

        const sensorIdFilter = measurement.sensorId.toLowerCase();
        const idFilter = measurement.id.toLowerCase();
        const dateFilter = measurement.date.toLowerCase();
        const typeFilter = measurement.type.toLowerCase();

        return sensorIdFilter.includes(this.filterText.toLowerCase()) ||
            idFilter.includes(this.filterText.toLowerCase()) ||
            dateFilter.includes(this.filterText.toLowerCase()) ||
            typeFilter.includes(this.filterText.toLowerCase())
      })
    }
  },
  methods: {
    getMeasurements: function () {
      const url = 'http://localhost:8888/api/measurement'
      axios.get(url)
          .then(response => {
            console.log(JSON.stringify(response.data))
            this.measurements = response.data
            console.log(this.measurements)
          })
          .catch(function (error) {
            console.log(error)
          })
    },

    saveFile: function () {
      const data = JSON.stringify(this.measurements)
      const blob = new Blob([data], {type: 'text/plain'})
      const e = document.createEvent('MouseEvents'),
          a = document.createElement('a');
      a.download = "measurements.json";
      a.href = window.URL.createObjectURL(blob);
      a.dataset.downloadurl = ['text/json', a.download, a.href].join(':');
      e.initEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
      a.dispatchEvent(e);
    }
  }
  // async mounted() {
  //   // fetch the data from the REST API when the component is mounted
  //   await axios.get('https://http://127.0.0.1:8090/api/measurment')
  //     .then(response => {
  //       this.measurements = response.data
  //     })
  // }
}
</script>