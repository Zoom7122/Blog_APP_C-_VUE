<template>
  <div class="article-view">
    <div class="search-container">
      <input v-model="properties.Title" placeholder="Введите название">
      <input v-model="properties.Tag" placeholder="Введите тег">
      <button @click="FindByProperties()">Найти по свойствам</button>
    </div>

    <!-- Сообщение если статьи не найдены -->
    <div v-if="ArticleList.length === 0 && !loading" class="no-articles">
      Статьи не найдены. Попробуйте изменить критерии поиска.
    </div>

    <!-- Отображение списка статей -->
    <div v-if="ArticleList.length > 0">
      <div class="articles-count">Найдено статей: {{ ArticleList.length }}</div>
      
      <div v-for="(article, index) in ArticleList" :key="index" class="article-item">
        <div class="article-header">
          <h2 class="article-title">{{ article.Title || 'Без названия' }}</h2>
          <div class="article-meta">
            <span class="author">Автор: {{ article.Author_Name || 'Неизвестный автор' }}</span>
            <span class="date" v-if="article.PublishedAt">
              {{ formatDate(article.PublishedAt) }}
            </span>
            <span class="tag" v-if="article.Tag">#{{ article.Tag }}</span>
            <span class="read-time" v-if="article.ReadTime">Время чтения: {{ article.ReadTime }} мин.</span>
          </div>
        </div>
        
        <div class="article-description" v-if="article.Description">
          {{ article.Description }}
        </div>
        
        <div class="article-content">
          <p>{{ article.Text || 'Содержание статьи отсутствует' }}</p>
        </div>
        
        <div class="article-footer">
          <span class="email" v-if="article.Author_Email">Email автора: {{ article.Author_Email }}</span>
        </div>

        <AddComment :articleId="article.Id"></AddComment>
        
        <button v-show="!showComments" 
        @click="showComments = true"
        class="showCommentsButton" >Показать комментарии к статье</button>

        <div v-show="showComments" class="divComments">
        <div class="countComments"> Колличество камментариев: {{ article.comments.length }}</div>

        <div v-for="(com, comIndex) in article.comments">
          <div class="Comments">
            <p>Комментарий : {{ comIndex + 1 }}</p>
            <h5 class="nameAuthorConnent"> {{ com.UserName }}</h5>
            <p class="contentComments">{{ com.Content }}</p>
            <p class="timeComments"> Опубликован в {{ com.CreatedAt || "1212"}}</p>
          </div>
        </div>

        <button v-show="showComments" 
          @click="showComments = false"
          class="closeCommentsButton">Скрыть комментарии к статье</button>

        </div>
        
        <hr v-if="index < ArticleList.length - 1">
      </div>
    </div>

    <!-- Индикатор загрузки -->
    <div v-if="loading" class="loading">
      Загрузка статей...
    </div>
  </div>
</template>


<script>
import AddComment from './AddComment.vue';
import api from '@/axios-config';

export default {
  components:{AddComment},
  data() {
    return {
      ArticleList: [],
      
      showComments: false,

      properties: {
        Title: null,
        Tag: null
      }
    }
  },
  
  methods: {
    formatDate(dateString) {
      if (!dateString) return '';
      const date = new Date(dateString)
      return date.toLocaleDateString('ru-RU', {
        day: 'numeric',
        month: 'long',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
      })
    },
    
    async FindByProperties() {
      try {
        console.log('Пользователь ввел: ' + this.properties.Title)
        const response = await api.post('ArticleConrtroller/FindByProperties', this.properties)

        console.log('Ответ от API: ', response.data)
        console.log('Успех: ' + response.data.success)

        if (response.data.success && response.data.list) {
          
          if (response.data.list.length > 0) {
            
            console.log('Полученные данные: ', response.data.list)

          }

          this.ArticleList = response.data.list.map(item => {
            const article = {
                Id: item.id || '',
                Title: item.title || '',
                Text: item.text || '',
                Tag: item.tag || '',
                Description: item.description || '',
                Cover_image: item.cover_image || '',
                Author_Name: item.author_Name || '',
                Author_Email: item.author_Email || '',
                ReadTime: item.readTime || 0,
                PublishedAt: item.publishedAt || null,
                comments: item.comments ? item.comments.map(com => ({
                  UserName: com.userName || 'Нет имени',
                  Content: com.content || 'Нет текста',
                  CreatedAt: com.createdAt.slice(0,10) || ''
                })) : []
            }
           console.log('Преобразованный элемент: ', article);

            console.log('Структура полученного элемента:', Object.keys(item));
            return article;
          });
          
          console.log('Массив статей: ', this.ArticleList)
        } else {
          console.log('Статьи не найдены: ' + (response.data.messegeEror || response.data.messageError || ''))
          this.ArticleList = []
        }
      }
      catch(err) {
        console.log('Ошибка Api: ', err)
        this.ArticleList = []
      }
    }
  }
}
</script>

<style scoped>
.article-view {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
}

.search-container {
  background: #f8f9fa;
  padding: 25px;
  border-radius: 15px;
  margin-bottom: 30px;
  box-shadow: 0 4px 15px rgba(0,0,0,0.05);
  display: flex;
  gap: 15px;
  flex-wrap: wrap;
  align-items: center;
}

.search-container input {
  flex: 1;
  min-width: 200px;
  padding: 12px 20px;
  border: 2px solid #e9ecef;
  border-radius: 8px;
  font-size: 16px;
  transition: all 0.3s ease;
  outline: none;
}

.search-container input:focus {
  border-color: #42b883;
  box-shadow: 0 0 0 3px rgba(66, 184, 131, 0.1);
}

.search-container button {
  padding: 12px 30px;
  background: #42b883;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: bold;
  font-size: 16px;
  transition: all 0.3s ease;
  white-space: nowrap;
}

.search-container button:hover {
  background: #3aa873;
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(66, 184, 131, 0.3);
}

.no-articles {
  text-align: center;
  padding: 40px;
  background: #fff3cd;
  border: 1px solid #ffeaa7;
  border-radius: 10px;
  color: #856404;
  font-size: 18px;
  margin: 20px 0;
}

.articles-count {
  font-size: 18px;
  color: #666;
  margin-bottom: 25px;
  padding-bottom: 15px;
  border-bottom: 2px solid #42b883;
  font-weight: 600;
}

.article-item {
  background: white;
  padding: 30px;
  border-radius: 15px;
  margin-bottom: 25px;
  box-shadow: 0 4px 20px rgba(0,0,0,0.08);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  border-left: 4px solid #42b883;
}

.article-item:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 30px rgba(0,0,0,0.12);
}

.article-header {
  margin-bottom: 20px;
}

.article-title {
  color: #2c3e50;
  margin: 0 0 15px 0;
  font-size: 28px;
  font-weight: 700;
  line-height: 1.3;
}

.article-meta {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  color: #666;
  font-size: 14px;
  padding-bottom: 15px;
  border-bottom: 1px solid #e9ecef;
}

.article-meta span {
  display: inline-flex;
  align-items: center;
  gap: 5px;
}

.article-meta .author {
  color: #42b883;
  font-weight: 600;
}

.article-meta .date {
  background: #f8f9fa;
  padding: 4px 10px;
  border-radius: 12px;
}

.article-meta .tag {
  background: #e3f2fd;
  color: #1976d2;
  padding: 4px 12px;
  border-radius: 12px;
  font-weight: 500;
}

.article-meta .read-time {
  color: #666;
  font-style: italic;
}

.article-description {
  font-size: 18px;
  color: #495057;
  line-height: 1.6;
  margin: 20px 0;
  padding: 15px;
  background: #f8f9fa;
  border-radius: 8px;
  border-left: 3px solid #42b883;
}

.article-content {
  margin: 25px 0;
  line-height: 1.8;
  color: #2c3e50;
  font-size: 16px;
}

.article-content p {
  margin: 0;
  white-space: pre-line;
}

.article-footer {
  margin-top: 25px;
  padding-top: 20px;
  border-top: 1px solid #e9ecef;
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 15px;
}

.article-footer .email {
  color: #666;
  font-size: 14px;
  background: #f8f9fa;
  padding: 8px 15px;
  border-radius: 8px;
}

.cover-image {
  max-width: 300px;
  border-radius: 10px;
  overflow: hidden;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
}

.cover-image img {
  width: 100%;
  height: auto;
  display: block;
  transition: transform 0.3s ease;
}

.cover-image img:hover {
  transform: scale(1.03);
}

.loading {
  text-align: center;
  padding: 40px;
  font-size: 18px;
  color: #42b883;
  font-weight: 600;
}

hr {
  border: none;
  border-top: 1px solid #e9ecef;
  margin: 30px 0 0 0;
}

/* Адаптивность */
@media (max-width: 768px) {
  .article-view {
    padding: 15px;
  }
  
  .search-container {
    flex-direction: column;
    padding: 20px;
  }
  
  .search-container input {
    width: 100%;
    min-width: unset;
  }
  
  .search-container button {
    width: 100%;
  }
  
  .article-item {
    padding: 20px;
  }
  
  .article-title {
    font-size: 24px;
  }
  
  .article-meta {
    gap: 10px;
  }
  
  .article-footer {
    flex-direction: column;
    align-items: flex-start;
  }
  
  .cover-image {
    max-width: 100%;
    margin-top: 15px;
  }
}

@media (max-width: 480px) {
  .article-meta {
    flex-direction: column;
    gap: 8px;
  }
  
  .article-title {
    font-size: 22px;
  }
  
  .article-description {
    font-size: 16px;
  }
}

/* Стили для блока комментариев */
.showCommentsButton {
  margin-top: 20px;
  padding: 12px 24px;
  background: linear-gradient(135deg, #42b883 0%, #3aa873 100%);
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  font-size: 16px;
  transition: all 0.3s ease;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  box-shadow: 0 4px 12px rgba(66, 184, 131, 0.25);
}

.showCommentsButton:hover {
  background: linear-gradient(135deg, #3aa873 0%, #329c66 100%);
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(66, 184, 131, 0.35);
}

.showCommentsButton:active {
  transform: translateY(0);
}

.divComments {
  margin-top: 25px;
  padding: 25px;
  background: #f8f9fa;
  border-radius: 12px;
  border-left: 4px solid #42b883;
  animation: fadeIn 0.3s ease-out;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(-10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.countComments {
  font-size: 18px;
  color: #2c3e50;
  font-weight: 600;
  margin-bottom: 20px;
  padding-bottom: 15px;
  border-bottom: 2px solid #e9ecef;
  display: flex;
  align-items: center;
  gap: 10px;
}

.countComments::before {
  content: "💬";
  font-size: 20px;
}

.Comments {
  background: white;
  padding: 20px;
  border-radius: 10px;
  margin-bottom: 15px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.05);
  transition: transform 0.2s ease, box-shadow 0.2s ease;
  border: 1px solid #e9ecef;
}

.Comments:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0,0,0,0.08);
}

.Comments > p:first-child {
  font-size: 12px;
  color: #42b883;
  font-weight: 600;
  margin-bottom: 8px;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.nameAuthorComment {
  color: #2c3e50;
  margin: 0 0 12px 0;
  font-size: 16px;
  font-weight: 700;
  display: flex;
  align-items: center;
  gap: 8px;
}

.nameAuthorComment::before {
  content: "👤";
  font-size: 14px;
}

.contentComments {
  color: #495057;
  line-height: 1.7;
  margin: 15px 0;
  padding: 15px;
  background: #f8f9fa;
  border-radius: 8px;
  border-left: 3px solid #e9ecef;
  font-size: 15px;
  white-space: pre-line;
}

.timeComments {
  font-size: 13px;
  color: #6c757d;
  margin-top: 10px;
  font-style: italic;
  display: flex;
  align-items: center;
  gap: 6px;
}

.timeComments::before {
  content: "🕒";
  font-size: 12px;
}

.closeCommentsButton {
  margin-top: 20px;
  padding: 12px 24px;
  background: linear-gradient(135deg, #6c757d 0%, #495057 100%);
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  font-size: 16px;
  transition: all 0.3s ease;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  box-shadow: 0 4px 12px rgba(108, 117, 125, 0.25);
  width: 100%;
}

.closeCommentsButton:hover {
  background: linear-gradient(135deg, #495057 0%, #343a40 100%);
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(108, 117, 125, 0.35);
}

.closeCommentsButton:active {
  transform: translateY(0);
}

@media (max-width: 768px) {
  .divComments {
    padding: 20px 15px;
  }
  
  .Comments {
    padding: 15px;
  }
  
  .contentComments {
    padding: 12px;
    font-size: 14px;
  }
  
  .showCommentsButton,
  .closeCommentsButton {
    width: 100%;
    padding: 14px 20px;
  }
}

@media (max-width: 480px) {
  .divComments {
    padding: 15px 12px;
  }
  
  .countComments {
    font-size: 16px;
  }
  
  .nameAuthorComment {
    font-size: 15px;
  }
  
  .contentComments {
    padding: 10px;
    font-size: 13px;
  }
  
  .timeComments {
    font-size: 12px;
  }
}


.divComments {
  animation: slideIn 0.4s ease-out;
}

@keyframes slideIn {
  from {
    opacity: 0;
    max-height: 0;
    overflow: hidden;
  }
  to {
    opacity: 1;
    max-height: 2000px;
  }
}



.showCommentsButton::after {
  content: " ▼";
  font-size: 12px;
  transition: transform 0.3s ease;
}

.closeCommentsButton::before {
  content: "▲ ";
  font-size: 12px;
}


.Comments.loading::after {
  content: "";
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 20px;
  height: 20px;
  border: 2px solid #f3f3f3;
  border-top: 2px solid #42b883;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  0% { transform: translate(-50%, -50%) rotate(0deg); }
  100% { transform: translate(-50%, -50%) rotate(360deg); }
}
</style>