﻿namespace SessionServer.Sessions;

public static class SessionManager
{
    private const string LOGIN_SESSION_KEY = "LoginSession";

    public static void SetLoginSession(ISession session, LoginSession loginSession)
    {
        session.SetString(LOGIN_SESSION_KEY, loginSession.ToString());
    }

    public static LoginSession GetLoginSession(ISession session)
    {
        var json = session.GetString(LOGIN_SESSION_KEY);
        return json == null ? null : LoginSession.FromJson(json);
    }

    public static void ClearLoginSession(ISession session)
    {
        session.Remove(LOGIN_SESSION_KEY);
    }
}