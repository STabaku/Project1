// ================= AUTH CONFIG =================
const TOKEN_KEY = "token";
const ROLE_KEY = "userRole";

// ================= PARSE JWT =================
function parseJwt(token) {
  try {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );

    return JSON.parse(jsonPayload);
  } catch {
    return null;
  }
}

// ================= AUTH GUARD =================
function authGuard(requiredRole = null) {
  const token = localStorage.getItem(TOKEN_KEY);

  if (!token) {
    window.location.replace("login.html");
    return;
  }

  const payload = parseJwt(token);

  if (!payload || !payload.exp) {
    logout();
    return;
  }

  const now = Math.floor(Date.now() / 1000);

  if (payload.exp < now) {
    logout();
    return;
  }

  if (requiredRole && payload.role !== requiredRole) {
    window.location.replace("index.html");
  }
}

// ================= LOGOUT =================
function logout() {
  localStorage.removeItem(TOKEN_KEY);
  localStorage.removeItem(ROLE_KEY);
  window.location.replace("login.html");
}
