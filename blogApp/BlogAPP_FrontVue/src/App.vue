<template>
  <div class="app-container">
    <!-- Текущая страница -->
    <div class="page-content">
      <Login 
        v-if="currentPage === 'login' && !isLoggedIn" 
        @success="handleLoginSuccess"
        @navigate-to-register="currentPage = 'register'"
      />
      
      <Registration 
        v-if="currentPage === 'register' && !isLoggedIn" 
        @registration-success="handleRegistrationSuccess"
        @navigate-to-login="currentPage = 'login'"
      />
      
      <!-- После успешного входа -->
      <Dashboard 
        v-if="isLoggedIn" 
        :user="userData"
        @logout="handleLogout"
      />
    </div>

    <nav class="app-nav" v-if="!isLoggedIn">
      <button 
        @click="currentPage = 'login'" 
        :class="{ active: currentPage === 'login' }"
        class="nav-btn"
      >
        Вход
      </button>
      <button 
        @click="currentPage = 'register'" 
        :class="{ active: currentPage === 'register' }"
        class="nav-btn"
      >
        Регистрация
      </button>
    </nav>
    
    <!-- Информация о подключении -->
    <InfAboutConnecting v-if="!isLoggedIn" />
  </div>
</template>

<script>
import Login from './components/Login.vue';
import Registration from './components/RegisterPage.vue';
import Dashboard from './components/Dashboard.vue';
import InfAboutConnecting from './components/InfAboutConnecting.vue';

export default {
  name: 'App',
  components: {
    Login,
    Registration,
    Dashboard,
    InfAboutConnecting,
  },
  data() {
    return {
      currentPage: 'login',
      isLoggedIn: false,
      userData: null
    };
  },
  methods: {
    handleLoginSuccess(userData) {
      this.isLoggedIn = true;
      this.userData = userData;
      console.log('Вход выполнен:', userData);
    },
    handleRegistrationSuccess(registrationData) {
      console.log('Регистрация успешна:', registrationData);
      // После регистрации переключаем на страницу входа
      this.currentPage = 'login';
      // Можно показать сообщение
      alert('Регистрация успешна! Теперь вы можете войти в систему.');
    },
    handleLogout() {
      this.isLoggedIn = false;
      this.userData = null;
      this.currentPage = 'login';
      console.log('Выход выполнен');
    },
    ArticleAddPage(){

    }
  },
  mounted() {
    // Проверяем, есть ли сохраненный токен или данные пользователя
    const savedToken = localStorage.getItem('auth_token');
    if (savedToken) {
      // Можно проверить токен и автоматически войти
      this.isLoggedIn = true;
    }
  }
};
</script>

<style scoped>
.app-container {
  max-width: 1000px;
  margin: 0 auto;
  padding: 20px;
  min-height: 100vh;
}

.app-nav {
  margin-top: 30px;
  display: flex;
  justify-content: center;
  gap: 10px;
  margin-bottom: 30px;
  padding-bottom: 20px;
  border-bottom: 1px solid #eee;
}

.nav-btn {
  padding: 12px 30px;
  background: transparent;
  color: #666;
  border: 2px solid #ddd;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s;
}

.nav-btn:hover {
  background: #f5f5f5;
  border-color: #bbb;
}

.nav-btn.active {
  background: #42b883;
  color: white;
  border-color: #42b883;
}

.page-content {
  min-height: 500px;
}

/* Адаптивность */
@media (max-width: 768px) {
  .app-container {
    padding: 15px;
  }
  
  .app-nav {
    flex-direction: column;
    align-items: center;
  }
  
  .nav-btn {
    width: 100%;
    max-width: 300px;
    margin-bottom: 10px;
  }
}
</style>