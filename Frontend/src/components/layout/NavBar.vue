<template>
    <v-app>
        <v-app-bar app>
            <!-- Menu button for small screens -->
            <v-app-bar-nav-icon v-if="isSmallScreen" @click="drawer = !drawer"></v-app-bar-nav-icon>

            <v-toolbar-title>Movie Library</v-toolbar-title>

            <!-- Tabs for larger screens -->
            <v-tabs v-if="!isSmallScreen" v-model="activeTab">
                <v-tab v-for="item in items" :key="item" :to="item.route" >{{ item.text }}</v-tab>
            </v-tabs>

        </v-app-bar>

        <!-- Responsive Navigation Drawer -->
        <v-navigation-drawer app v-model="drawer" temporary>
            <router-link v-for="(item, index) in items" :key="index" :to="item.route" link>
                <div>
                    <v-btn :class="{ 'nav-list': true, 'active': item.route === activeTab }" style="width: 100%;">
                        {{ item.text }}
                    </v-btn>
                </div>
            </router-link>
        </v-navigation-drawer>

        <!-- Main content -->
        <v-main>
            <!-- Your main content goes here -->
            <router-view></router-view>
        </v-main>
    </v-app>
</template>

<script>
export default {
    data() {
        return {
            items: [
                { text: 'Movies', route: '/movies' },
                { text: 'Add Movie', route: '/movies/add' },
            ],
            drawer: false,
            isSmallScreen: false,
            activeTab: '/movies', // Initial active tab 
        };
    },
    methods: {
        checkScreenSize() {
            // Update the flag based on screen width
            this.isSmallScreen = window.innerWidth <= 600;
        },
    },
    mounted() {
        this.checkScreenSize();
        window.addEventListener('resize', this.checkScreenSize);
    },
    watch: {
        $route() {
            // Update active tab when route changes
            this.activeTab = this.$route.path;
        },
        drawer()
        {
            // update the activeTab when navigation Drawer gets opened, bcoz when navigation Drawer gets opened the activeTab's underline is not coming
            this.activeTab = this.$route.path;
        }
    },
};
</script>

<style scoped>
.nav-list {
    width: 100%;
    text-align: center;
    height: 40px;
    margin: 4px;
    padding: 5px;
    color: black;
    text-decoration: none !important;
}

.active {
    border-bottom: 3px solid black;
}
</style>
