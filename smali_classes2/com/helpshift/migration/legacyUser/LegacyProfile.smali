.class public Lcom/helpshift/migration/legacyUser/LegacyProfile;
.super Ljava/lang/Object;
.source "LegacyProfile.java"


# instance fields
.field public final email:Ljava/lang/String;

.field public final identifier:Ljava/lang/String;

.field public final migrationState:Lcom/helpshift/migration/MigrationState;

.field public final name:Ljava/lang/String;

.field public final serverId:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/migration/MigrationState;)V
    .locals 0

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 17
    iput-object p1, p0, Lcom/helpshift/migration/legacyUser/LegacyProfile;->identifier:Ljava/lang/String;

    .line 18
    iput-object p2, p0, Lcom/helpshift/migration/legacyUser/LegacyProfile;->email:Ljava/lang/String;

    .line 19
    iput-object p3, p0, Lcom/helpshift/migration/legacyUser/LegacyProfile;->name:Ljava/lang/String;

    .line 20
    iput-object p4, p0, Lcom/helpshift/migration/legacyUser/LegacyProfile;->serverId:Ljava/lang/String;

    .line 21
    iput-object p5, p0, Lcom/helpshift/migration/legacyUser/LegacyProfile;->migrationState:Lcom/helpshift/migration/MigrationState;

    return-void
.end method
