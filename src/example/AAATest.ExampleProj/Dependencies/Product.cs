﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.ExampleProj.Dependencies {
	public class Product {
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsAvailable { get; set; }
		public User ManagedBy { get; set; }
	}
}
