.class public final Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;
.super Lcom/zopim/android/sdk/store/a;

# interfaces
.implements Lcom/zopim/android/sdk/store/VisitorInfoStorage;


# static fields
.field private static final EMAIL_KEY:Ljava/lang/String; = "email_key"

.field private static final LOG_TAG:Ljava/lang/String; = "VisitorInfoPrefsStorage"

.field private static final NAME_KEY:Ljava/lang/String; = "name_key"

.field private static final PHONE_NUMBER_KEY:Ljava/lang/String; = "phone_number_key"

.field private static final PREFS_NAME:Ljava/lang/String; = "visitor_info"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>(Landroid/content/Context;)V
    .locals 1

    const-string v0, "visitor_info"

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

.method public getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;
    .locals 5

    iget-boolean v0, p0, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;->mDisabled:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;->mStoragePreferences:Landroid/content/SharedPreferences;

    const-string v2, "email_key"

    invoke-interface {v0, v2, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iget-object v2, p0, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;->mStoragePreferences:Landroid/content/SharedPreferences;

    const-string v3, "name_key"

    invoke-interface {v2, v3, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    iget-object v3, p0, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;->mStoragePreferences:Landroid/content/SharedPreferences;

    const-string v4, "phone_number_key"

    invoke-interface {v3, v4, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-nez v0, :cond_1

    if-nez v2, :cond_1

    if-nez v3, :cond_1

    return-object v1

    :cond_1
    new-instance v1, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    invoke-direct {v1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;-><init>()V

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->email(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    move-result-object v0

    invoke-virtual {v0, v2}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->name(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    move-result-object v0

    invoke-virtual {v0, v3}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->phoneNumber(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->build()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    return-object v0
.end method

.method public setVisitorInfo(Lcom/zopim/android/sdk/model/VisitorInfo;)V
    .locals 4

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Visitor info must not be null. Skipping storing visitor info."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iget-boolean v0, p0, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;->mDisabled:Z

    if-eqz v0, :cond_1

    sget-object p1, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Storage is disabled, will abort storing visitor info"

    invoke-static {p1, v0}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;->mStoragePreferences:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getEmail()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getPhoneNumber()Ljava/lang/String;

    move-result-object p1

    if-eqz v1, :cond_2

    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v3

    if-nez v3, :cond_2

    const-string v3, "email_key"

    invoke-interface {v0, v3, v1}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    :cond_2
    if-eqz v2, :cond_3

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_3

    const-string v1, "name_key"

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    :cond_3
    if-eqz p1, :cond_4

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_4

    const-string v1, "phone_number_key"

    invoke-interface {v0, v1, p1}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    :cond_4
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method
