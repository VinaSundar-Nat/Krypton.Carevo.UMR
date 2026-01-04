# Database Diagram

```mermaid
erDiagram
    User ||--o{ UserEmployer : "works for"
    User ||--o{ UserSkill : "has skills"
    User ||--o{ Contact : "has contacts"
    User ||--|| Address : "owns (embedded)"
    User ||--o{ Application : "applies with"
    User ||--o{ Streak : "maintains"
    User ||--o{ Project : "owns individual"
    
    Employer ||--o{ UserEmployer : "employs"
    
    UserEmployer ||--o{ Project : "has projects"
    
    Project ||--o{ ProjectSkill : "requires"

    Skill ||--o{ UserSkill : "assigned to users"
    Skill ||--o{ ProjectSkill : "assigned to projects"

    Application ||--o{ ApplicationStatusHistory : "has history"

    User {
        int Id PK
        string FirstName
        string LastName
        DateTime Dob
        UserStatus Status
        DateTime CreatedAt
        string CreatedBy
        uint VersionStamp
        string Address_Line1 "Owned"
        string Address_Line2 "Owned"
        string Address_Suburb "Owned"
        string Address_City "Owned"
        string Address_State "Owned"
        string Address_PostCode "Owned"
        string Address_Country "Owned"
        point Address_Coordinates "Owned"
    }

    Address {
        string Line1
        string Line2
        string Suburb
        string City
        string State
        string PostCode
        string Country
        Coordinates Coordinates
    }

    Employer {
        int Id PK
        string Company
        string Url
        string Logo
        DateTime CreatedAt
        string CreatedBy
        uint VersionStamp
    }

    UserEmployer {
        int Id PK
        int UserId FK
        int EmployerId FK
        DateTime StartDate
        DateTime EndDate
    }

    Project {
        int Id PK
        string Title
        string Description
        int UserEmployerId FK "nullable"
        int UserId FK "nullable"
        DateTime CreatedAt
        string CreatedBy
        uint VersionStamp
    }

    Skill {
        int Id PK
        string Code
        string Description
        DateTime EffectiveDate
        DateTime CreatedAt
        string CreatedBy
        uint VersionStamp
    }

    UserSkill {
        int UserId FK
        int SkillId FK
    }

    ProjectSkill {
        int ProjectId FK
        int SkillId FK
    }

    Application {
        int Id PK
        int JobId
        int UserId FK
        ApplicationStatus Status
        DateTime AppliedDate
        string Notes
        string PersonalizedEmploymentData
        DateTime CreatedAt
        string CreatedBy
        uint VersionStamp
    }

    ApplicationStatusHistory {
        int Id PK
        ApplicationStatus Status
        ApplicationStatus PreviousStatus
        DateTime StatusChangedDate
        string Notes
        string ChangedBy
        string Reason
        int ApplicationId FK
        DateTime CreatedAt
        string CreatedBy
        uint VersionStamp
    }

    Streak {
        int Id PK
        DateTime ActivityDate
        int ApplicationCount
        int ConsecutiveDayCount "nullable"
        int UserId FK
        DateTime CreatedAt
        string CreatedBy
        uint VersionStamp
    }

    Contact {
        int Id PK
        ContactType Type
        string Value
        int UserId FK "Owned"
        DateTime CreatedAt
        string CreatedBy
        uint VersionStamp
    }
```

## Enums

### UserStatus
| Value | Description |
|-------|-------------|
| Active | User is active |
| Inactive | User is inactive |
| Suspended | User is suspended |
| Pending | User registration pending |

### ApplicationStatus
| Value | Description |
|-------|-------------|
| Applied | Application submitted |
| UnderReview | Under review by employer |
| Shortlisted | Candidate shortlisted |
| Interviewed | Candidate interviewed |
| Accepted | Application accepted |
| Rejected | Application rejected |
| Withdrawn | Application withdrawn by user |
| Saved | Application saved (draft) |
| Archived | Application archived |

### ContactType
| Value | Description |
|-------|-------------|
| Email | Email address |
| Phone | Phone number |
| Mobile | Mobile number |

## Notes

- **Address** is an owned entity embedded in the `User` table (columns prefixed with `Address_`)
- **Contact** is an owned collection stored in a separate `user_contacts` table
- **UserSkill** and **ProjectSkill** are join tables for many-to-many relationships
- **Project** can belong to either a `UserEmployer` (employment project) or directly to a `User` (individual project)
- All entities inherit audit fields: `CreatedAt`, `CreatedBy`, `VersionStamp`

