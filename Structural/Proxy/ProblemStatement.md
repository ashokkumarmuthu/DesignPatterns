# Proxy Pattern Challenge: Secure Confidential Document Vault 🔐

### Scenario
You are building a document management system for a corporation that handles sensitive information. Documents have classification levels: **Public**, **Internal**, **Confidential**, and **Restricted**.

Access to documents must be controlled based on the user's role:
| Classification | Allowed Roles |
|:---|:---|
| **Public** | Everyone (Guest, Employee, Manager, Admin) |
| **Internal** | Employee, Manager, Admin |
| **Confidential** | Manager, Admin |
| **Restricted** | Admin only |

Additionally, **every access attempt** (successful or denied) must be logged in an audit trail for compliance.

The real document vault (`ConfidentialDocumentVault`) stores and retrieves documents but has no concept of access control or logging. A `SecureDocumentProxy` wraps the vault and transparently adds both features.

---

### What it teaches:
* **The Proxy Design Pattern:** Providing a surrogate for another object to control access, add behavior, or defer operations.
* **Protection Proxy:** Role-based access control without modifying the real subject.
* **Logging Proxy:** Audit trail generation transparently through the proxy layer.

---

### Core Requirements
1. **Subject Interface (`IDocumentVault`):**
   - `GetDocument(documentId, user)` → returns document or null
   - `ListDocuments(user)` → returns list of document IDs
2. **Real Subject (`ConfidentialDocumentVault`):**
   - Stores documents in memory, retrieves by ID
3. **Proxy (`SecureDocumentProxy`):**
   - Checks user role against document classification before granting access
   - Logs all access attempts (granted and denied)
   - Provides `PrintAuditLog()` for compliance review
4. **Domain Models:**
   - `Document`: ID, title, classification, content
   - `UserContext`: User ID, name, role
   - `AccessLogEntry`: Timestamp, user info, document ID, action, granted/denied
