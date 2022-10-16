import { defineComponent, h, PropType } from "vue"
import { Bar } from "vue-chartjs"
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale,
  Plugin
} from "chart.js"

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

export interface Data {
  data: Array<number>
}

export interface ChartData {
  labels: Array<string>
  datasets: Array<Data>
}

export default defineComponent({
  name: "BarChart",
  components: { Bar },
  props: {
    chartData: {
      type: Object as PropType<ChartData>
    },
    chartId: {
      type: String,
      default: "bar-chart"
    },
    width: {
      type: Number,
      default: 800
    },
    height: {
      type: Number,
      default: 200
    },
    cssClasses: {
      default: "",
      type: String
    },
    styles: {
      type: Object as PropType<Partial<CSSStyleDeclaration>>,
      default: () => {}
    },
    plugins: {
      type: Object as PropType<Plugin<"bar">>,
      default: () => {}
    }
  },
  setup(props) {
    const chartData = {
      labels: ["15.10.2022", "16.10.2022"],
      datasets: [
        {
          data: [2, 1]
        }
      ]
    }

    const chartOptions = { responsive: true }

    return () =>
      h(Bar, {
        chartData: props.chartData,
        chartOptions,
        chartId: props.chartId,
        width: props.width,
        height: props.height,
        cssClasses: props.cssClasses,
        styles: props.styles,
        plugins: props.plugins
      })
  }
})
