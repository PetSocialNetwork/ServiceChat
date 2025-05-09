﻿using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Exceptions;
using ServiceChat.Domain.Interfaces;

namespace ServiceChat.Domain.Services
{
    public class ChatService
    {
        private readonly IChatRepository _chatRepository;
        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository
                ?? throw new ArgumentNullException(nameof(chatRepository));
        }

        public async Task<Chat> GetChatByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _chatRepository.GetById(id, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new ChatNotFoundException("Чат не существует.");
            }
        }

        public async Task<Chat> AddChatAsync(Chat chat, CancellationToken cancellationToken)
        {
            if (await ChatExistsAsync(chat.FriendIds, cancellationToken))
            {
                throw new ChatAlreadyExistsException("Чат с данным пользователем уже существует.");
            }

            ArgumentNullException.ThrowIfNull(chat);
            await _chatRepository.Add(chat, cancellationToken);
            return chat;
        }

        public async Task<Chat> GetOrCreateChatAsync(Chat chat, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(chat);
            var existingChat = await _chatRepository.GetChatByUsersAsync(chat.FriendIds, cancellationToken);
            if (existingChat != null)
            { 
                return existingChat;
            }

            await _chatRepository.Add(chat, cancellationToken);
            return chat;
        }

        public async Task DeleteChatAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedChat = await _chatRepository.FindChatAsync(id, cancellationToken);
            if (existedChat is null)
            {
                throw new ChatNotFoundException("Чат не существует.");
            }

            await _chatRepository.Delete(existedChat, cancellationToken);
        }

        public async Task<List<Chat>> BySearchAsync(Guid chatId, int take, int offset, CancellationToken cancellationToken)
        {
            if (take < 0 || offset < 0)
            {
                throw new ArgumentException("Параметр не может быть меньше 0");
            }
            return await _chatRepository.BySearch(chatId, take, offset, cancellationToken);
        }

        private async Task<bool> ChatExistsAsync(List<Guid> friendIds, CancellationToken cancellationToken)
        {
            var existingChat = await _chatRepository.GetChatByUsersAsync(friendIds, cancellationToken);

            return existingChat != null;
        }
    }
}
