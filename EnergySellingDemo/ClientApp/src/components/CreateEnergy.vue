<template>

    <div class="vue-tempalte">
        <form v-on:submit.prevent="submitFormEnergySellingDetails">
            <h3>Add Energy Details</h3>
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="date" class="float-left">Date</label>
                        <input type="date" id="date" class="form-control" v-model="form.date">
                    </div>
                    <div class="form-group">
                        <label for="type" class="float-left">Select Energy type</label>
                        <select name="type" v-model="form.type" id="type" class="form-control">
                            <option disabled value="">Please select one</option>
                            <option>Electricity</option>
                            <option>Gas</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="float-left" for="price">Price of Energy</label>
                        <input name="price" v-model="form.price" id="price" type="number" class="form-control form-control-lg" />
                    </div>
                    <button type="submit" class="btn btn-dark btn-lg btn-block">Save</button>
                </div>
                <div class="col-md-4">
                </div>
            </div>



        </form>
    </div>
</template>

<script>
    import axios from 'axios'
    export default {
        name: "CreateEnergy",
        data() {
            return {
                form: {
                    date: new Date(),
                    type: '',
                    price: 0,
                }
            };
        },
        methods: {
            submitFormEnergySellingDetails() {
                axios.post('/EnergySelling', this.form)
                    .then((response) => {
                        console.log(response.data)
                        this.energysellings = response.data;
                        this.$router.push('/EnergySellingData');
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        }
    }

</script>