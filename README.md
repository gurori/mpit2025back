# Инструкция по запуску
## Шаг 1: Установка
Скопируйте все исходные файлы прокета.

## Шаг 2: Запуск приложения

1. Запуск контейнеров. Выполните следующую команду в терминале из корневой папки проекта (где находится docker-compose.yml):
```vs
   docker-compose up --build
```
   Опция --build перезапустит сборку контейнеров перед запуском. Это полезно, если вы вносили изменения в проект.

2. Если все прошло успешно, в терминале вы увидите вывод логов. Подождите, пока контейнеры полностью запустятся.

## Шаг 3: Проверьте работу приложения

Ваш API теперь доступен по адресу http://localhost:8080, если вы использовали проброс порта, указанный в docker-compose.yml. Откройте ваш браузер или используйте инструменты, такие как Swagger, чтобы протестировать API.
