.class public final Lcom/zopim/android/sdk/store/MachineIdPrefsStorage;
.super Lcom/zopim/android/sdk/store/a;

# interfaces
.implements Lcom/zopim/android/sdk/store/MachineIdStorage;


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "MachineIdPrefsStorage"

.field private static final MACHINE_ID_KEY:Ljava/lang/String; = "stored_machine_id"

.field private static final PREFS_NAME:Ljava/lang/String; = "machine_id"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>(Landroid/content/Context;)V
    .locals 1

    const-string v0, "machine_id"

    invoke-direct {p0, p1, v0}, Lcom/zopim/android/sdk/store/a;-><init>(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic delete()V
    .locals 0

    invoke-super {p0}, Lcom/zopim/android/sdk/store/a;->delete()V

    return-void
.end method

.method public bridge synthetic disable()V
    .locals 0

    invoke-super {p0}, Lcom/zopim/android/sdk/store/a;->disable()V

    return-void
.end method

.method public getMachineId()Ljava/lang/String;
    .locals 3

    iget-boolean v0, p0, Lcom/zopim/android/sdk/store/MachineIdPrefsStorage;->mDisabled:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/store/MachineIdPrefsStorage;->mStoragePreferences:Landroid/content/SharedPreferences;

    const-string v2, "stored_machine_id"

    invoke-interface {v0, v2, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public setMachineId(Ljava/lang/String;)V
    .locals 2

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/store/MachineIdPrefsStorage;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Machine id must not be null. Skipping storing machine id."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iget-boolean v0, p0, Lcom/zopim/android/sdk/store/MachineIdPrefsStorage;->mDisabled:Z

    if-eqz v0, :cond_1

    sget-object p1, Lcom/zopim/android/sdk/store/MachineIdPrefsStorage;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Storage is disabled, will abort storing machine id  "

    invoke-static {p1, v0}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/store/MachineIdPrefsStorage;->mStoragePreferences:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    const-string v1, "stored_machine_id"

    invoke-interface {v0, v1, p1}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {p1}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method
