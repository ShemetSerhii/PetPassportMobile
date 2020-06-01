using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using PetPassport.Models;

namespace PetPassport.Services
{
    public class PetClient : ApiClient
    {
        public async Task<IEnumerable<Pet>> GetPetsAsync()
        {
            var ownerId = User?.Id;

            var response = await HttpClient.GetAsync($"{Url}/Animal/{ownerId}/animals");

            return await ReadContentAsync<IEnumerable<Pet>>(response);
        }

        public async Task<PetInfo> GetPetAsync(Guid petId)
        {
            var response = await HttpClient.GetAsync($"{Url}/Animal/{petId}");

            return await ReadContentAsync<PetInfo>(response);
        }

        public async Task<IEnumerable<MedicalRow>> GetMedicalsRowsAsync(Guid petId)
        {
            var response = await HttpClient.GetAsync($"{Url}/Medical/{petId}");

            return await ReadContentAsync<IEnumerable<MedicalRow>>(response);
        }

        public async Task<bool> AddPetAsync(PetCreate pet)
        {
            var ownerId = User?.Id;

            var content = CreateContent(pet);

            var response = await HttpClient.PostAsync($"{Url}/Animal/{ownerId}", content);

            return response.StatusCode == HttpStatusCode.OK;
        }


        public async Task DownloadFile(AttachmentDto attachment)
        {
            var response = await HttpClient.GetAsync($"{Url}/Attachment/{attachment.Id}");

            var directory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
            var file = Path.Combine(directory, attachment.FileName);

            var bytes = await response.Content.ReadAsByteArrayAsync();

            File.WriteAllBytes(file, bytes);
        }
    }
}