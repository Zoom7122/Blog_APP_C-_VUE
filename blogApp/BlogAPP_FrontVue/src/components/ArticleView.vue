<template>
  <div class="article-view">
    <!-- Заголовок статьи -->
    <div class="article-header">
      <h1 class="article-title">{{ article.title }}</h1>
      <div class="article-meta">
        <span class="article-author">{{ article.authorName }}</span>
        <span class="article-date">{{ formatDate(article.createdAt) }}</span>
        <span class="article-views">👁 {{ article.views }}</span>
      </div>
      <!-- Теги -->
      <div class="article-tags" v-if="article.tags && article.tags.length">
        <span 
          v-for="tag in article.tags" 
          :key="tag" 
          class="tag"
        >
          #{{ tag }}
        </span>
      </div>
    </div>

    <!-- Контент статьи -->
    <div class="article-content">
      <div class="content" v-html="article.content"></div>
    </div>

    <!-- Комментарии -->
    <div class="comments-section">
      <h3>Комментарии ({{ comments.length }})</h3>
      
      <!-- Форма добавления комментария -->
      <div class="comment-form" v-if="isLoggedIn">
        <textarea 
          v-model="newComment" 
          placeholder="Напишите комментарий..."
          class="comment-input"
        ></textarea>
        <button 
          @click="addComment" 
          class="submit-btn"
          :disabled="!newComment.trim()"
        >
          Отправить
        </button>
      </div>
      
      <!-- Список комментариев -->
      <div class="comments-list">
        <div 
          v-for="comment in comments" 
          :key="comment.id" 
          class="comment-item"
        >
          <div class="comment-header">
            <span class="comment-author">{{ comment.authorName }}</span>
            <span class="comment-date">{{ formatDate(comment.createdAt) }}</span>
          </div>
          <div class="comment-text">{{ comment.text }}</div>
        </div>
        
        <div v-if="comments.length === 0" class="no-comments">
          Пока нет комментариев. Будьте первым!
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ArticleView',
  
  props: {
    articleId: {
      type: [String, Number],
      required: true
    }
  },
  
  data() {
    return {
      article: {
        id: 1,
        title: 'Название статьи',
        content: '<p>Содержание статьи. Здесь может быть <strong>форматированный</strong> текст.</p>',
        authorName: 'Автор статьи',
        createdAt: '2024-01-15T10:30:00',
        views: 156,
        tags: ['технологии', 'программирование', 'vue']
      },
      
      comments: [
        { id: 1, authorName: 'Пользователь 1', text: 'Отличная статья!', createdAt: '2024-01-15T12:00:00' },
        { id: 2, authorName: 'Пользователь 2', text: 'Спасибо за информацию', createdAt: '2024-01-16T09:15:00' }
      ],
      
      newComment: '',
      isLoggedIn: true // В реальном приложении проверять из store/auth
    }
  },
  
  methods: {
    formatDate(dateString) {
      const date = new Date(dateString)
      return date.toLocaleDateString('ru-RU', {
        day: 'numeric',
        month: 'long',
        year: 'numeric'
      })
    },
    
    addComment() {
      if (!this.newComment.trim()) return
      
      const newComment = {
        id: Date.now(),
        authorName: 'Текущий пользователь',
        text: this.newComment,
        createdAt: new Date().toISOString()
      }
      
      this.comments.push(newComment)
      this.newComment = ''
    }
  },
  
  mounted() {
    // В реальном приложении здесь был бы запрос к API
    console.log('Загружаем статью с ID:', this.articleId)
  }
}
</script>

<style scoped>
.article-view {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}

.article-header {
  margin-bottom: 30px;
  padding-bottom: 20px;
  border-bottom: 2px solid #eee;
}

.article-title {
  font-size: 32px;
  margin: 0 0 15px 0;
  color: #333;
}

.article-meta {
  display: flex;
  gap: 15px;
  margin-bottom: 15px;
  color: #666;
  font-size: 14px;
}

.article-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
  margin-top: 10px;
}

.tag {
  background: #e9ecef;
  color: #495057;
  padding: 4px 10px;
  border-radius: 15px;
  font-size: 13px;
  cursor: pointer;
}

.tag:hover {
  background: #dee2e6;
}

.article-content {
  font-size: 16px;
  line-height: 1.6;
  margin-bottom: 40px;
}

.content {
  min-height: 200px;
}

.comments-section {
  border-top: 2px solid #eee;
  padding-top: 20px;
}

.comments-section h3 {
  margin-bottom: 20px;
  color: #333;
}

.comment-form {
  margin-bottom: 30px;
}

.comment-input {
  width: 100%;
  min-height: 80px;
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 6px;
  font-size: 14px;
  margin-bottom: 10px;
  resize: vertical;
}

.comment-input:focus {
  outline: none;
  border-color: #42b883;
}

.submit-btn {
  padding: 8px 20px;
  background: #42b883;
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
}

.submit-btn:disabled {
  background: #ccc;
  cursor: not-allowed;
}

.submit-btn:hover:not(:disabled) {
  background: #3aa876;
}

.comments-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.comment-item {
  padding: 15px;
  border: 1px solid #eee;
  border-radius: 8px;
  background: #f9f9f9;
}

.comment-header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 10px;
  font-size: 13px;
  color: #666;
}

.comment-author {
  font-weight: 600;
}

.comment-text {
  font-size: 14px;
  line-height: 1.5;
}

.no-comments {
  text-align: center;
  color: #888;
  padding: 30px;
  font-style: italic;
}

/* Адаптивность */
@media (max-width: 768px) {
  .article-view {
    padding: 15px;
  }
  
  .article-title {
    font-size: 24px;
  }
  
  .article-meta {
    flex-direction: column;
    gap: 5px;
  }
}
</style>