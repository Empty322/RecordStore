using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RecordStore.Controllers
{
    public class ApiController : Controller
    {
		private readonly ICountryRepository countryRepository;
		private readonly IArtistRepository artistRepository;
		private readonly IRecordRepository recordRepository;
		private readonly IGenreRepository genreRepository;

		public ApiController(ICountryRepository countryRepository, IArtistRepository artistRepository, IRecordRepository recordRepository, IGenreRepository genreRepository)
		{
			this.countryRepository = countryRepository;
			this.artistRepository = artistRepository;
			this.recordRepository = recordRepository;
			this.genreRepository = genreRepository;
		}

		[Produces("application/json")]
		[HttpGet("/api/GetGenres")]
		public IActionResult GetGenres() {
			return Ok(JsonConvert.SerializeObject(genreRepository.GetAll().Select(g => g.Id)));
		}

		[Produces("application/json")]
		[HttpGet("/api/GetCountries")]
		public IActionResult GetCountries()
		{
			return Ok(JsonConvert.SerializeObject(countryRepository.GetAll().Select(c => c.CountryName)));
		}

		[Produces("application/json")]
		[HttpGet("/api/GetArtists")]
		public IActionResult GetArtists()
		{
			return Ok(JsonConvert.SerializeObject(artistRepository.GetAll()));
		}

		[Produces("application/json")]
		[HttpGet("/api/GetRecords")]
		public IActionResult GetRecords() {
			List<Record> records = recordRepository.GetAll().ToList();
			records.ForEach(record => record.Artist.Records = null);
			return Ok(JsonConvert.SerializeObject(records));
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("/api/DeleteGenre/{genreId}")]
		public IActionResult DeleteGenre(string genreId)
		{
			Genre genre = genreRepository.GetAll().FirstOrDefault(g => g.Id == genreId);
			genreRepository.Delete(genre);
			return Ok("success");
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("/api/DeleteCountry/{countryName}")]
		public IActionResult DeleteCountry(string countryName)
		{
			Country country = countryRepository.GetAll().FirstOrDefault(c => c.CountryName == countryName);
			countryRepository.Delete(country);
			return Ok("success");
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("/api/DeleteArtist/{artistId}")]
		public IActionResult DeleteArtist(int artistId)
		{
			Artist artist = artistRepository.GetById(artistId);
			artistRepository.Delete(artist);
			return Ok("success");
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("/api/DeleteRecord/{recordId}")]
		public IActionResult DeleteRecord(int recordId)
		{
			Record record = recordRepository.GetById(recordId);
			recordRepository.Delete(record);
			return Ok("success");
		}
	}
}