.class Lcom/helpshift/migration/RemoteDataMigrator$2;
.super Lcom/helpshift/common/domain/F;
.source "RemoteDataMigrator.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/migration/RemoteDataMigrator;->updateProfileMigrationStateUpdate(Lcom/helpshift/migration/MigrationState;Lcom/helpshift/migration/MigrationState;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/migration/RemoteDataMigrator;

.field final synthetic val$newState:Lcom/helpshift/migration/MigrationState;

.field final synthetic val$oldState:Lcom/helpshift/migration/MigrationState;


# direct methods
.method constructor <init>(Lcom/helpshift/migration/RemoteDataMigrator;Lcom/helpshift/migration/MigrationState;Lcom/helpshift/migration/MigrationState;)V
    .locals 0

    .line 190
    iput-object p1, p0, Lcom/helpshift/migration/RemoteDataMigrator$2;->this$0:Lcom/helpshift/migration/RemoteDataMigrator;

    iput-object p2, p0, Lcom/helpshift/migration/RemoteDataMigrator$2;->val$oldState:Lcom/helpshift/migration/MigrationState;

    iput-object p3, p0, Lcom/helpshift/migration/RemoteDataMigrator$2;->val$newState:Lcom/helpshift/migration/MigrationState;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 193
    iget-object v0, p0, Lcom/helpshift/migration/RemoteDataMigrator$2;->this$0:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-static {v0}, Lcom/helpshift/migration/RemoteDataMigrator;->access$200(Lcom/helpshift/migration/RemoteDataMigrator;)Ljava/lang/ref/WeakReference;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 194
    iget-object v0, p0, Lcom/helpshift/migration/RemoteDataMigrator$2;->this$0:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-static {v0}, Lcom/helpshift/migration/RemoteDataMigrator;->access$200(Lcom/helpshift/migration/RemoteDataMigrator;)Ljava/lang/ref/WeakReference;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/migration/RemoteDataMigrator$RemoteDataMigratorListener;

    iget-object v1, p0, Lcom/helpshift/migration/RemoteDataMigrator$2;->this$0:Lcom/helpshift/migration/RemoteDataMigrator;

    invoke-static {v1}, Lcom/helpshift/migration/RemoteDataMigrator;->access$300(Lcom/helpshift/migration/RemoteDataMigrator;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/migration/RemoteDataMigrator$2;->val$oldState:Lcom/helpshift/migration/MigrationState;

    iget-object v3, p0, Lcom/helpshift/migration/RemoteDataMigrator$2;->val$newState:Lcom/helpshift/migration/MigrationState;

    invoke-interface {v0, v1, v2, v3}, Lcom/helpshift/migration/RemoteDataMigrator$RemoteDataMigratorListener;->onMigrationStateChanged(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/migration/MigrationState;Lcom/helpshift/migration/MigrationState;)V

    :cond_0
    return-void
.end method
