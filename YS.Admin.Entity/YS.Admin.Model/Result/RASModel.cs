﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Admin.Model.Result
{
	public class RASModel
	{
		public RASModel() { }
		public string PrivateKey { get; set; }
		public string PublicKey { get; set; }

		public long ExpireTime { get; set; }
	}
}
