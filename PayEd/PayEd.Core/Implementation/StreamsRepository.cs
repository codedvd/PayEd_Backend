using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PayEd.Core.Services;
using PayEd.Data.AppContext;
using PayEd.Data.Dto;
using PayEd.Data.Models;
using PayEd.Infrastructure.Helpers;
using Supabase.Gotrue;

namespace PayEd.Core.Implementation
{
    public class StreamsRepository : IStreamRepository
    {
        private readonly AppDbContext _context;
        public StreamsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> CreateStreamAsync(Guid userId, StreamDto income)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Id == userId);

            if (user == null)
            {
                return ApiResponse.Error("User with Id does not exist");
            }

            var randomStreamCode = GenerateRandomAlphaNumericString(8);
            var newStream = new Streams
            {
                Stream_Id = Guid.NewGuid(),
                Stream_name = income.Stream_name,
                Description = income.Description,
                StreamCode = randomStreamCode,
                UserId = user.User_Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                isDeleted = false
            };

            _context.Streams.Add(newStream);
            await _context.SaveChangesAsync();

            return ApiResponse.Success(newStream, "Stream created successfully");
        }

        private string GenerateRandomAlphaNumericString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(characters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return result;
        }

        public async Task<ApiResponse> DeleteStreamAsync(Guid streamID)
        {
            var checkForStream = await _context.Streams.FirstOrDefaultAsync(d => d.Stream_Id == streamID && !d.isDeleted);

            if (checkForStream == null)
            {
                return ApiResponse.Error("Expense with this ID does not exist");
            }
            checkForStream.isDeleted = true;
            checkForStream.UpdatedAt = DateTime.UtcNow;

            _context.Streams.Update(checkForStream);
            await _context.SaveChangesAsync();

            return ApiResponse.Success(checkForStream.Stream_Id, "Expense successfully deleted");
        }

        public async Task<ApiResponse> GetStreamAsync(Guid streamID)
        {
            var expense = await _context.Streams
               .Include(e => e.User) // Include the associated User entity
               .FirstOrDefaultAsync(dat => dat.Stream_Id == streamID && !dat.isDeleted);

            if (expense == null)
            {
                return ApiResponse.Error("Expense with this Id does not exist");
            }

            return ApiResponse.Success(new
            {
                Stream_name = expense.Stream_name,
                Description = expense.Description,
                Stream_code = expense.StreamCode,
                CreatedAt = expense.CreatedAt,
                Stream_Id = expense.Stream_Id,
                User_Id = expense.UserId
            }, "Stream successfully retrieved");
        }

        public async Task<ApiResponse> GetStreamsAsync()
        {
            var expense = await _context.Streams
                .Where(d => !d.isDeleted)
                .Select(e => new
                {
                    Stream_name = e.Stream_name,
                    Description = e.Description,
                    Stream_code = e.StreamCode,
                    CreatedAt = e.CreatedAt,
                    Stream_Id = e.Stream_Id,
                    User_Id = e.UserId
                })
                .ToListAsync();
            if (expense == null || !expense.Any())
            {
                return ApiResponse.Error("Stream does not exist");
            }
            return ApiResponse.Success(expense, "All Stream successfully retrieved");
        }

        public async Task<ApiResponse> UpdateStreamAsync(Guid streamID, StreamDto stream)
        {
            var existingStream = await _context.Streams.FirstOrDefaultAsync(d => d.Stream_Id == streamID && !d.isDeleted);
            if (existingStream == null)
            {
                return ApiResponse.Error("Stream with this ID does not exist");
            }

            existingStream.Stream_name = stream.Stream_name;
            existingStream.Description = stream.Description;
            existingStream.UpdatedAt = DateTime.UtcNow;
            existingStream.isDeleted = false;

            try
            {
                _context.Streams.Update(existingStream);
                await _context.SaveChangesAsync();

                return ApiResponse.Success(existingStream, "Stream successfully updated");
            }
            catch (DbUpdateException ex)
            {
                return ApiResponse.Error("Failed to update the stream: " + ex.Message);
            }
        }
    }
}
