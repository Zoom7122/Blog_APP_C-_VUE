<template>
  <div class="article-view">

    <input v-model="properties.Title" placeholder="Введите название">
    <button @click="FindByProperties()">Найти по свойствам</button>

    <!-- Заголовок статьи -->
    <div>
      <h1 >{{ article.Titile }}</h1>
      <div >
        <span >{{ article.Author_Name }}</span>
        <span >{{ formatDate(article.createdAt) }}</span>
        <span >{{ article.tag }}</span>
      </div>
    </div>

  <div>{{ article.Text }}</div>

  </div>
</template>

<script>
import api from '@/axios-config';

export default {
  data() {
    return {
      article: {
        Titile:'',
        Text: '',
        Tag: '',
        Description: '',
        Cover_image:'',
        Author_Name:'',
        Author_Email:'',
        ReadTime: '',
        PublishedAt: null,
      },
      
      properties:{
        Title: null,
        Tag: null
      },
      isLoggedIn: true 
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
    async FindByProperties() {
      try
      {
      console.log('Пользователь ввел: ' + this.properties.inputTitile)
      const response = await api.post('/ArticleView/FindByProperties', this.properties)
      
      //this.article = response.data.

      console.log('Ответ от API ' + response.data.success)

      console.log('Ответ от API подробно ' + response.data.list)
      }
      catch(err)
      {
        console.log('Ошибка Api: ' + err)
      }
    }
  }
}
</script>

<style scoped>

</style>