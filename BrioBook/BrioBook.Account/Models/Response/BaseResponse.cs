﻿namespace BrioBook.Client.Models.Response;

public abstract class BaseResponse
{
    public bool Succeeded { get; set; }
    public string Errors { get; set; }
}