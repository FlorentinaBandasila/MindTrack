﻿using MindTrack.Models.Data;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MindTrack.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MindTrack.Services.Repositories
{
    public class EmotionRepository : IEmotionRepository
    {
        private readonly MindTrackContext _mindTrackContext;
        public EmotionRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<Emotion>> GetAllEmotions()
        {
            return await _mindTrackContext.Emotions.ToListAsync();
        }

        public async Task<Emotion> GetEmotionById(Guid id)
        {
            return await _mindTrackContext.Emotions.FirstOrDefaultAsync(u => u.Emotion_id == id);
        }

        public async Task CreateEmotion(Emotion emotion)
        {
            await _mindTrackContext.Emotions.AddAsync(emotion);
            await _mindTrackContext.SaveChangesAsync();
        }

        public async Task<List<MoodCountDTO>> GetUserEmotionsGroupedByMood(Guid userId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var emotions = await _mindTrackContext.Emotions
                .Where(e => e.User_id == userId && e.Date >= startDate && e.Date < endDate)
                .Include(e => e.Mood_selection)
                .ToListAsync();

            var latestEmotionsPerDay = emotions
                .GroupBy(e => e.Date.Date)
                .Select(g => g.OrderByDescending(e => e.Date).FirstOrDefault())
                .Where(e => e != null && e.Mood_selection != null)
                .ToList();

            var result = latestEmotionsPerDay
                .GroupBy(e => e.Mood_selection.Mood)
                .Select(g => new MoodCountDTO
                {
                    MoodName = g.Key,
                    Count = g.Count()
                })
                .ToList();

            return result;
        }


        public async Task<List<JournalDTO>> GetMoodByUser(Guid userId)
        {
            return await _mindTrackContext.Emotions
                .Where(e => e.User_id == userId)
                .Include(e => e.Mood_selection)
                .Select(e => new JournalDTO
                {
                    Mood_Name = e.Mood_selection.Mood,
                    Reflection = e.Reflection,
                    Date = e.Date
                }).OrderByDescending(e => e.Date)
                .ToListAsync();
        }




        public async Task<List<MoodDTO>> GetMoodByDay(Guid userId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var emotions = await _mindTrackContext.Emotions
                .Where(e => e.User_id == userId && e.Date >= startDate && e.Date < endDate)
                .Include(e => e.Mood_selection)
                .ToListAsync();

            var dailyMoods = emotions
                .GroupBy(e => e.Date.Date)
                .Select(g => g.OrderByDescending(e => e.Date).FirstOrDefault())
                .Where(e => e != null && e.Mood_selection != null)
                .Select(e => new MoodDTO
                {
                    Mood_Name = e.Mood_selection.Mood,
                    Date = e.Date
                })
                .ToList();

            return dailyMoods;
        }

        public async Task DeleteEmotion(Guid id)
        {
            var emotion = await _mindTrackContext.Emotions.FindAsync(id);

            if (emotion != null) _mindTrackContext.Emotions.Remove(emotion);

            await _mindTrackContext.SaveChangesAsync();

        }

        public async Task<Emotion?> GetEmotionByUserIdAndDate(Guid userId, DateTime date)
        {
            return await _mindTrackContext.Emotions
                .Where(e => e.User_id == userId && e.Date.Date == date.Date)
                .FirstOrDefaultAsync();
        }

    }
}
