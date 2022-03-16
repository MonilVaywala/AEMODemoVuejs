<template>
    <h1 id="tableLabel">Energy Selling</h1>
    
    <div class="row">
        <div class="col-md-12 mb-2 ">
            <button class="btn btn-primary float-right mr-1" @click="createEnergy">
                Add Energy
            </button>
        </div>
    </div>
    <table class='table table-striped' aria-labelledby="tableLabel" v-if="energysellings">
        <thead>
            <tr>
                <th>Date</th>
                <th>Energy Type</th>
                <th>Price</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="energyselling of energysellings" v-bind:key="energyselling">
                <td>{{ new Date(energyselling.date).toLocaleDateString("en-ZA") }}</td>
                <td>{{ energyselling.type }}</td>
                <td style="display:inline-flex;">${{ energyselling.price }}<p v-if="energyselling.isDiscount">(10% Discount)</p></td>
            </tr>
        </tbody>
    </table>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "EnergySellingData",
        data() {
            return {
                energysellings: []
            }
        },
        methods: {
            getEnergySelling() {
                axios.get('/energyselling')
                    .then((response) => {
                        console.log(response.data)
                        this.energysellings = response.data;
                        
                        
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            createEnergy() {
                this.$router.push('/CreateEnergy');
            }
        },
        
        mounted() {
            this.getEnergySelling();
        }
    }
</script>