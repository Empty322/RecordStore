﻿using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IRecordRepository : IRepository<Record, int>, IHasImage
    {
		IEnumerable<Record> GetRecordsByType(string type);
	}
}
