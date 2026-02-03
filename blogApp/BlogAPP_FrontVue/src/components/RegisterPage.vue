<template>
  <div class="registration-container">
    <h1>Регистрация</h1>
    <p>Создайте новый аккаунт для доступа к системе</p>
    
    <!-- Форма регистрации -->
    <div class="form">
      <!-- Имя пользователя -->
      <div class="form-group">
        <label>Имя пользователя:</label>
        <input 
          type="text" 
          v-model="firstName"
          placeholder="Иван"
          class="input-field"
          :class="{ 'error-input': errors.firstName }"
        />
        <div v-if="errors.firstName" class="error-message">
          {{ errors.firstName }}
        </div>
      </div>
      
      <!-- Email -->
      <div class="form-group">
        <label>Email адрес:</label>
        <input 
          type="email" 
          v-model="email"
          placeholder="ivan@example.com"
          class="input-field"
          :class="{ 'error-input': errors.email }"
        />
        <div v-if="errors.email" class="error-message">
          {{ errors.email }}
        </div>
      </div>
      
      <!-- Пароль -->
      <div class="form-group">
        <label>Пароль:</label>
        <input 
          type="password" 
          v-model="password"
          placeholder="Минимум 8 символов"
          class="input-field"
          :class="{ 'error-input': errors.password }"
        />
        <div v-if="errors.password" class="error-message">
          {{ errors.password }}
        </div>
      </div>
      
      <!-- Подтверждение пароля -->
      <div class="form-group">
        <label>Подтверждение пароля:</label>
        <input 
          type="password" 
          v-model="confirmPassword"
          placeholder="Повторите пароль"
          class="input-field"
          :class="{ 'error-input': errors.confirmPassword }"
        />
        <div v-if="errors.confirmPassword" class="error-message">
          {{ errors.confirmPassword }}
        </div>
      </div>
      
      <!-- Аватар (опционально) -->
      <div class="form-group">
        <label>Ссылка на аватар (необязательно):</label>
        <input 
          type="text" 
          v-model="avatar_url"
          placeholder="https://example.com/avatar.jpg"
          class="input-field"
        />
      </div>
      
      <!-- Биография (опционально) -->
      <div class="form-group">
        <label>О себе (необязательно):</label>
        <textarea 
          v-model="bio"
          placeholder="Расскажите немного о себе..."
          class="input-field textarea"
          rows="3"
        ></textarea>
      </div>
      
      <!-- Роль (обычно скрыто или select) -->
      <div class="form-group" v-if="showRoleSelect">
        <label>Роль:</label>
        <select v-model="role" class="input-field">
          <option value="User">Пользователь</option>
          <option value="Admin">Администратор</option>
          <option value="Moderator">Модератор</option>
        </select>
      </div>
      
      <!-- Кнопки -->
      <div class="form-actions">
        <button 
          @click="register" 
          :disabled="loading || !isFormValid"
          class="submit-btn"
        >
          {{ loading ? 'Регистрация...' : 'Зарегистрироваться' }}
        </button>
        
        <button 
          @click="goToLogin" 
          class="secondary-btn"
        >
          Уже есть аккаунт? Войти
        </button>
      </div>
    </div>
    
    <!-- Сообщение об успехе -->
    <div v-if="successMessage" class="success-box">
      <h3>✅ Успешная регистрация!</h3>
      <p>{{ successMessage }}</p>
      <button @click="goToLoginAfterSuccess" class="success-btn">
        Перейти к входу
      </button>
    </div>
    
    <!-- Ошибка регистрации -->
    <div v-if="registrationError" class="error-box">
      <h3>Ошибка регистрации:</h3>
      <p>{{ registrationError }}</p>
      <div v-if="validationErrors.length > 0" class="validation-errors">
        <ul>
          <li v-for="(error, index) in validationErrors" :key="index">
            {{ error }}
          </li>
        </ul>
      </div>
    </div>
    
    <div v-if="debug && responseData" class="debug-box">
      <h4>Ответ сервера:</h4>
      <pre>{{ responseData }}</pre>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'Registration',
  props: {
    showRoleSelect: {
      type: Boolean,
      default: false
    },
    debug: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      firstName: '',
      email: '',
      password: '',
      confirmPassword: '',
      avatar_url: '',
      bio: '',
      role: 'User',
      
      loading: false,
      successMessage: '',
      registrationError: '',
      responseData: null,
      validationErrors: [],
      
      errors: {
        firstName: '',
        email: '',
        password: '',
        confirmPassword: ''
      }
    }
  },
  computed: {
    isFormValid() {
      return (
        this.firstName.trim().length >= 2 &&
        this.email.trim() &&
        this.validateEmail(this.email) &&
        this.password.length >= 8 &&
        this.password === this.confirmPassword
      )
    }
  },
  watch: {
    firstName(newVal) {
      if (newVal.length > 0 && newVal.length < 2) {
        this.errors.firstName = 'Имя должно содержать минимум 2 символа';
      } else {
        this.errors.firstName = '';
      }
    },
    email(newVal) {
      if (newVal && !this.validateEmail(newVal)) {
        this.errors.email = 'Введите корректный email';
      } else {
        this.errors.email = '';
      }
    },
    password(newVal) {
      if (newVal && newVal.length < 8) {
        this.errors.password = 'Пароль должен содержать минимум 8 символов';
      } else {
        this.errors.password = '';
      }
    },
    confirmPassword(newVal) {
      if (newVal && this.password && newVal !== this.password) {
        this.errors.confirmPassword = 'Пароли не совпадают';
      } else {
        this.errors.confirmPassword = '';
      }
    }
  },
  methods: {
    validateEmail(email) {
      const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return re.test(email);
    },
    
    validateForm() {
      this.validationErrors = [];
      
      if (this.firstName.trim().length < 2) {
        this.validationErrors.push('Имя должно содержать минимум 2 символа');
      }
      
      if (!this.email.trim()) {
        this.validationErrors.push('Email обязателен');
      } else if (!this.validateEmail(this.email)) {
        this.validationErrors.push('Введите корректный email');
      }
      
      if (this.password.length < 8) {
        this.validationErrors.push('Пароль должен содержать минимум 8 символов');
      }
      
      if (this.password !== this.confirmPassword) {
        this.validationErrors.push('Пароли не совпадают');
      }
      
      return this.validationErrors.length === 0;
    },
    
    async register() {
      // Сброс предыдущих сообщений
      this.successMessage = '';
      this.registrationError = '';
      this.responseData = null;
      this.validationErrors = [];
      
      // Валидация формы
      if (!this.validateForm()) {
        this.registrationError = 'Пожалуйста, исправьте ошибки в форме';
        return;
      }
      
      this.loading = true;
      
      // Данные для отправки
      const registrationData = {
        firstName: this.firstName.trim(),
        email: this.email.trim(),
        password: this.password,
        avatar_url: this.avatar_url.trim() || null,
        bio: this.bio.trim() || null,
        role: this.role
      };
      
      console.log('Отправляю данные регистрации:', registrationData);
      
      try {
        const response = await axios.post(
          'https://localhost:7284/api/EntranceConroller/Register', 
          registrationData,
          {
            headers: {
              'Content-Type': 'application/json'
            }
          }
        );



        
        console.log('Ответ от сервера:', response.data);
        this.responseData = response.data;
        
        if (response.data.success) {

          this.successMessage = response.data.message || 'Регистрация прошла успешно!';
          
          // Очищаем форму
          this.resetForm();
          
          // Отправляем событие успешной регистрации
          this.$emit('registration-success', response.data);
          
        } else {
          // Сервер вернул ошибку
          this.registrationError = response.data.messegeEror || 'Ошибка регистрации';
          this.validationErrors = response.data.errors || [];
        }
        
      } catch (error) {
        console.error('Ошибка при регистрации:', error);
        this.handleRegistrationError(error);
        
      } finally {
        this.loading = false;
      }
    },
    
    handleRegistrationError(error) {
      if (error.response) {
        // Сервер ответил с ошибкой
        const status = error.response.status;
        const data = error.response.data;
        
        switch (status) {
          case 400:
            this.registrationError = data.message || 'Некорректные данные';
            this.validationErrors = data.errors || [];
            break;
          case 409:
            this.registrationError = 'Пользователь с таким email уже существует';
            break;
          case 422:
            this.registrationError = 'Ошибка валидации данных';
            this.validationErrors = data.errors || [];
            break;
          default:
            this.registrationError = `Ошибка сервера: ${status}`;
        }
        
      } else if (error.request) {
        // Запрос был отправлен, но ответа нет
        this.registrationError = 'Нет ответа от сервера. Проверьте подключение.';
        
      } else {
        // Другая ошибка
        this.registrationError = `Ошибка: ${error.message}`;
      }
    },
    
    resetForm() {
      this.firstName = '';
      this.email = '';
      this.password = '';
      this.confirmPassword = '';
      this.avatar_url = '';
      this.bio = '';
      this.role = 'User';
      this.errors = {
        firstName: '',
        email: '',
        password: '',
        confirmPassword: ''
      };
    },
    
    goToLogin() {
      this.$emit('navigate-to-login');
    },
    
    goToLoginAfterSuccess() {
      this.successMessage = '';
      this.$emit('navigate-to-login');
    }
  }
}
</script>

<style scoped>
.registration-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 30px;
  background: white;
  border-radius: 12px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
}

h1 {
  color: #333;
  margin-bottom: 10px;
  font-size: 28px;
  text-align: center;
}

p {
  color: #666;
  margin-bottom: 30px;
  text-align: center;
  font-size: 16px;
}

.form {
  margin: 30px 0;
  padding: 25px;
  background: #f9f9f9;
  border-radius: 10px;
  border: 1px solid #eee;
}

.form-group {
  margin-bottom: 25px;
}

.form-group label {
  display: block;
  margin-bottom: 8px;
  font-weight: 600;
  color: #444;
  font-size: 14px;
}

.input-field {
  width: 100%;
  padding: 12px 15px;
  border: 1px solid #ddd;
  border-radius: 6px;
  font-size: 16px;
  transition: all 0.3s;
  box-sizing: border-box;
}

.input-field:hover {
  border-color: #bbb;
}

.input-field:focus {
  outline: none;
  border-color: #42b883;
  box-shadow: 0 0 0 3px rgba(66, 184, 131, 0.2);
}

.input-field.error-input {
  border-color: #ff6b6b;
  background-color: #fff5f5;
}

.error-message {
  color: #ff6b6b;
  font-size: 13px;
  margin-top: 5px;
  font-style: italic;
}

.textarea {
  min-height: 80px;
  resize: vertical;
  font-family: inherit;
}

.form-actions {
  display: flex;
  flex-direction: column;
  gap: 15px;
  margin-top: 30px;
}

.submit-btn {
  padding: 14px 25px;
  background: linear-gradient(135deg, #42b883, #3aa876);
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s;
}

.submit-btn:hover:not(:disabled) {
  background: linear-gradient(135deg, #3aa876, #2e8b57);
  transform: translateY(-1px);
  box-shadow: 0 4px 12px rgba(66, 184, 131, 0.3);
}

.submit-btn:disabled {
  background: #ccc;
  cursor: not-allowed;
  transform: none;
}

.secondary-btn {
  padding: 12px 25px;
  background: transparent;
  color: #42b883;
  border: 2px solid #42b883;
  border-radius: 8px;
  font-size: 15px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s;
}

.secondary-btn:hover {
  background: rgba(66, 184, 131, 0.1);
}

/* Сообщения */
.success-box {
  margin: 30px 0;
  padding: 25px;
  background: linear-gradient(135deg, #e8f5e9, #c8e6c9);
  border-radius: 10px;
  border-left: 5px solid #4caf50;
  text-align: center;
}

.success-box h3 {
  color: #2e7d32;
  margin-top: 0;
  margin-bottom: 15px;
}

.success-box p {
  color: #388e3c;
  margin-bottom: 20px;
}

.success-btn {
  padding: 12px 25px;
  background: #4caf50;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 15px;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.3s;
}

.success-btn:hover {
  background: #3d8b40;
}

.error-box {
  margin: 30px 0;
  padding: 25px;
  background: linear-gradient(135deg, #ffebee, #ffcdd2);
  border-radius: 10px;
  border-left: 5px solid #f44336;
}

.error-box h3 {
  color: #c62828;
  margin-top: 0;
  margin-bottom: 15px;
}

.error-box p {
  color: #d32f2f;
  margin-bottom: 15px;
}

.validation-errors {
  margin-top: 15px;
  padding: 15px;
  background: rgba(255, 255, 255, 0.8);
  border-radius: 6px;
}

.validation-errors ul {
  margin: 0;
  padding-left: 20px;
}

.validation-errors li {
  color: #d32f2f;
  margin-bottom: 5px;
}

.debug-box {
  margin-top: 30px;
  padding: 20px;
  background: #f5f5f5;
  border-radius: 8px;
  border: 1px dashed #999;
}

.debug-box h4 {
  color: #666;
  margin-top: 0;
}

.debug-box pre {
  background: white;
  padding: 15px;
  border-radius: 6px;
  overflow: auto;
  font-family: 'Consolas', monospace;
  font-size: 13px;
  line-height: 1.4;
}

/* Адаптивность */
@media (max-width: 768px) {
  .registration-container {
    padding: 20px;
    margin: 10px;
  }
  
  .form {
    padding: 20px;
  }
  
  .form-actions {
    flex-direction: column;
  }
}

@media (max-width: 480px) {
  h1 {
    font-size: 24px;
  }
  
  .input-field,
  .submit-btn,
  .secondary-btn {
    font-size: 14px;
    padding: 10px 15px;
  }
}
</style>