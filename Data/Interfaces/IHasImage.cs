using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IHasImage
    {
		bool GetImageById(int id, out byte[] bytes, out string contentType);
	}
}
