.class public final Lnet/gogame/gowrap/support/SupportManager;
.super Ljava/lang/Object;
.source "SupportManager.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 33
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getCategories()Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/support/SupportCategory;",
            ">;"
        }
    .end annotation

    .line 37
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 38
    new-instance v1, Lnet/gogame/gowrap/support/SupportCategory;

    const-string v2, "billing"

    sget v3, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_category_billing:I

    invoke-direct {v1, v2, v3}, Lnet/gogame/gowrap/support/SupportCategory;-><init>(Ljava/lang/String;I)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 40
    new-instance v1, Lnet/gogame/gowrap/support/SupportCategory;

    const-string v2, "connection"

    sget v3, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_category_connection:I

    invoke-direct {v1, v2, v3}, Lnet/gogame/gowrap/support/SupportCategory;-><init>(Ljava/lang/String;I)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 42
    new-instance v1, Lnet/gogame/gowrap/support/SupportCategory;

    const-string v2, "game_play"

    sget v3, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_category_gameplay:I

    invoke-direct {v1, v2, v3}, Lnet/gogame/gowrap/support/SupportCategory;-><init>(Ljava/lang/String;I)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 44
    new-instance v1, Lnet/gogame/gowrap/support/SupportCategory;

    const-string v2, "feedback"

    sget v3, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_category_feedback:I

    invoke-direct {v1, v2, v3}, Lnet/gogame/gowrap/support/SupportCategory;-><init>(Ljava/lang/String;I)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 46
    new-instance v1, Lnet/gogame/gowrap/support/SupportCategory;

    const-string v2, "account_lost"

    sget v3, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_category_account_lost:I

    invoke-direct {v1, v2, v3}, Lnet/gogame/gowrap/support/SupportCategory;-><init>(Ljava/lang/String;I)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-object v0
.end method

.method private static getDeviceId(Landroid/content/Context;)Ljava/lang/String;
    .locals 1

    .line 171
    invoke-virtual {p0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p0

    const-string v0, "android_id"

    invoke-static {p0, v0}, Landroid/provider/Settings$Secure;->getString(Landroid/content/ContentResolver;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static getDeviceInfo()Ljava/lang/String;
    .locals 5

    .line 176
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v0

    const-string v1, "Brand=%s / Manufacturer=%s / Model=%s / Device=%s"

    const/4 v2, 0x4

    new-array v2, v2, [Ljava/lang/Object;

    sget-object v3, Landroid/os/Build;->BRAND:Ljava/lang/String;

    const/4 v4, 0x0

    aput-object v3, v2, v4

    sget-object v3, Landroid/os/Build;->MANUFACTURER:Ljava/lang/String;

    const/4 v4, 0x1

    aput-object v3, v2, v4

    sget-object v3, Landroid/os/Build;->MODEL:Ljava/lang/String;

    const/4 v4, 0x2

    aput-object v3, v2, v4

    sget-object v3, Landroid/os/Build;->DEVICE:Ljava/lang/String;

    const/4 v4, 0x3

    aput-object v3, v2, v4

    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static getExtraData(Landroid/content/Context;Ljava/lang/String;Lnet/gogame/gowrap/support/SupportCategory;)Ljava/lang/String;
    .locals 3

    .line 146
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 147
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Date/time: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    new-instance v2, Ljava/util/Date;

    invoke-direct {v2}, Ljava/util/Date;-><init>()V

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, "\n"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    if-eqz p1, :cond_0

    .line 149
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Name: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "\n"

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 151
    :cond_0
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Game: "

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p0}, Lnet/gogame/gowrap/support/AppInfo;->getAppLabel(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "\n"

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 152
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Version: "

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p0}, Lnet/gogame/gowrap/support/AppInfo;->getAppVersion(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "\n"

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 153
    sget-object p1, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {p1}, Lnet/gogame/gowrap/GoWrapImpl;->getGuid()Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 154
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Game user ID: "

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v1, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v1}, Lnet/gogame/gowrap/GoWrapImpl;->getGuid()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "\n"

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_1
    if-eqz p2, :cond_2

    .line 157
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Category: "

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Lnet/gogame/gowrap/support/SupportCategory;->getId()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, "\n"

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 159
    :cond_2
    sget-object p1, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {p1, p0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getCurrentLocale(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_3

    .line 160
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string p2, "Locale: "

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object p2, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {p2, p0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getCurrentLocale(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, "\n"

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 162
    :cond_3
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string p2, "Device ID: "

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p0}, Lnet/gogame/gowrap/support/SupportManager;->getDeviceId(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, "\n"

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 163
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string p2, "Mem: "

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p0}, Lnet/gogame/gowrap/support/SupportManager;->getMemoryInfo(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, "\n"

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 164
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string p2, "Device Type: "

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Lnet/gogame/gowrap/support/SupportManager;->getDeviceInfo()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, "\n"

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 165
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string p2, "OS Version: "

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Lnet/gogame/gowrap/support/SupportManager;->getOsVersion()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, "\n"

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 166
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string p2, "Resolution: "

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p0}, Lnet/gogame/gowrap/support/SupportManager;->getScreenResolution(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {p1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "\n"

    invoke-virtual {p1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 167
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static getMemoryInfo(Landroid/content/Context;)Ljava/lang/String;
    .locals 8

    .line 193
    new-instance v0, Landroid/app/ActivityManager$MemoryInfo;

    invoke-direct {v0}, Landroid/app/ActivityManager$MemoryInfo;-><init>()V

    const-string v1, "activity"

    .line 194
    invoke-virtual {p0, v1}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/app/ActivityManager;

    .line 196
    invoke-virtual {v1, v0}, Landroid/app/ActivityManager;->getMemoryInfo(Landroid/app/ActivityManager$MemoryInfo;)V

    .line 197
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/4 v2, 0x1

    const/4 v3, 0x0

    const/16 v4, 0x10

    if-lt v1, v4, :cond_0

    .line 198
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v1

    const-string v4, "Available=%s, Total=%s"

    const/4 v5, 0x2

    new-array v5, v5, [Ljava/lang/Object;

    iget-wide v6, v0, Landroid/app/ActivityManager$MemoryInfo;->availMem:J

    .line 200
    invoke-static {p0, v6, v7}, Landroid/text/format/Formatter;->formatShortFileSize(Landroid/content/Context;J)Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v3

    iget-wide v6, v0, Landroid/app/ActivityManager$MemoryInfo;->totalMem:J

    .line 201
    invoke-static {p0, v6, v7}, Landroid/text/format/Formatter;->formatShortFileSize(Landroid/content/Context;J)Ljava/lang/String;

    move-result-object p0

    aput-object p0, v5, v2

    .line 198
    invoke-static {v1, v4, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    return-object p0

    .line 203
    :cond_0
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v1

    const-string v4, "Available=%s"

    new-array v2, v2, [Ljava/lang/Object;

    iget-wide v5, v0, Landroid/app/ActivityManager$MemoryInfo;->availMem:J

    .line 205
    invoke-static {p0, v5, v6}, Landroid/text/format/Formatter;->formatShortFileSize(Landroid/content/Context;J)Ljava/lang/String;

    move-result-object p0

    aput-object p0, v2, v3

    .line 203
    invoke-static {v1, v4, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static getOsVersion()Ljava/lang/String;
    .locals 5

    .line 185
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v0

    const-string v1, "Android %s / SDK %d / %s"

    const/4 v2, 0x3

    new-array v2, v2, [Ljava/lang/Object;

    sget-object v3, Landroid/os/Build$VERSION;->RELEASE:Ljava/lang/String;

    const/4 v4, 0x0

    aput-object v3, v2, v4

    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I

    .line 188
    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    const/4 v4, 0x1

    aput-object v3, v2, v4

    sget-object v3, Landroid/os/Build;->FINGERPRINT:Ljava/lang/String;

    const/4 v4, 0x2

    aput-object v3, v2, v4

    .line 185
    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method private static getScreenResolution(Landroid/content/Context;)Ljava/lang/String;
    .locals 5

    const-string v0, "window"

    .line 210
    invoke-virtual {p0, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/view/WindowManager;

    .line 212
    new-instance v0, Landroid/util/DisplayMetrics;

    invoke-direct {v0}, Landroid/util/DisplayMetrics;-><init>()V

    .line 213
    invoke-interface {p0}, Landroid/view/WindowManager;->getDefaultDisplay()Landroid/view/Display;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/view/Display;->getMetrics(Landroid/util/DisplayMetrics;)V

    .line 214
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object p0

    const-string v1, "%dx%d, DPI: %d, Density: %.2f"

    const/4 v2, 0x4

    new-array v2, v2, [Ljava/lang/Object;

    iget v3, v0, Landroid/util/DisplayMetrics;->widthPixels:I

    .line 216
    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    iget v3, v0, Landroid/util/DisplayMetrics;->heightPixels:I

    .line 217
    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    const/4 v4, 0x1

    aput-object v3, v2, v4

    iget v3, v0, Landroid/util/DisplayMetrics;->densityDpi:I

    .line 218
    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    const/4 v4, 0x2

    aput-object v3, v2, v4

    iget v0, v0, Landroid/util/DisplayMetrics;->density:F

    .line 219
    invoke-static {v0}, Ljava/lang/Float;->valueOf(F)Ljava/lang/Float;

    move-result-object v0

    const/4 v3, 0x3

    aput-object v0, v2, v3

    .line 214
    invoke-static {p0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static humanReadableByteCount(JZ)Ljava/lang/String;
    .locals 9

    if-eqz p2, :cond_0

    const/16 v0, 0x3e8

    goto :goto_0

    :cond_0
    const/16 v0, 0x400

    :goto_0
    int-to-long v1, v0

    cmp-long v3, p0, v1

    if-gez v3, :cond_1

    .line 224
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p0, p1}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string p0, " B"

    invoke-virtual {p2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_1
    long-to-double p0, p0

    .line 225
    invoke-static {p0, p1}, Ljava/lang/Math;->log(D)D

    move-result-wide v1

    int-to-double v3, v0

    invoke-static {v3, v4}, Ljava/lang/Math;->log(D)D

    move-result-wide v5

    div-double/2addr v1, v5

    double-to-int v0, v1

    .line 226
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    if-eqz p2, :cond_2

    const-string v2, "kMGTPE"

    goto :goto_1

    :cond_2
    const-string v2, "KMGTPE"

    :goto_1
    add-int/lit8 v5, v0, -0x1

    invoke-virtual {v2, v5}, Ljava/lang/String;->charAt(I)C

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    if-eqz p2, :cond_3

    const-string p2, ""

    goto :goto_2

    :cond_3
    const-string p2, "i"

    :goto_2
    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    .line 227
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v1

    const-string v2, "%.1f %sB"

    const/4 v5, 0x2

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    int-to-double v7, v0

    .line 229
    invoke-static {v3, v4, v7, v8}, Ljava/lang/Math;->pow(DD)D

    move-result-wide v3

    invoke-static {p0, p1}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr p0, v3

    invoke-static {p0, p1}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p0

    aput-object p0, v5, v6

    const/4 p0, 0x1

    aput-object p2, v5, p0

    .line 227
    invoke-static {v1, v2, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static isValid(Lnet/gogame/gowrap/support/SupportRequest;)Z
    .locals 1

    .line 52
    invoke-virtual {p0}, Lnet/gogame/gowrap/support/SupportRequest;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 53
    invoke-virtual {p0}, Lnet/gogame/gowrap/support/SupportRequest;->getEmail()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 54
    invoke-virtual {p0}, Lnet/gogame/gowrap/support/SupportRequest;->getCategory()Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 55
    invoke-virtual {p0}, Lnet/gogame/gowrap/support/SupportRequest;->getCategory()Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/support/SupportCategory;->getId()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 56
    invoke-virtual {p0}, Lnet/gogame/gowrap/support/SupportRequest;->getBody()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    sget-object v0, Landroid/util/Patterns;->EMAIL_ADDRESS:Ljava/util/regex/Pattern;

    .line 57
    invoke-virtual {p0}, Lnet/gogame/gowrap/support/SupportRequest;->getEmail()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    invoke-virtual {p0}, Ljava/util/regex/Matcher;->matches()Z

    move-result p0

    if-eqz p0, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    return p0
.end method

.method public static send(Landroid/content/Context;Lnet/gogame/gowrap/support/SupportRequest;)Ljava/lang/Long;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lnet/gogame/gowrap/support/SupportServiceException;
        }
    .end annotation

    .line 63
    :try_start_0
    new-instance v0, Lnet/gogame/gowrap/support/MultipartUtility;

    const-string v1, "https://gw-support.gogame.net/support/v1/tickets"

    const-string v2, "UTF-8"

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/support/MultipartUtility;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 65
    sget-object v1, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_0

    const-string v1, "appId"

    .line 66
    sget-object v2, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v2}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    .line 68
    :cond_0
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getName()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_1

    const-string v1, "name"

    .line 69
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    .line 71
    :cond_1
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getEmail()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_2

    const-string v1, "email"

    .line 72
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    .line 74
    :cond_2
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getMobileNumber()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_3

    const-string v1, "mobileNumber"

    .line 75
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getMobileNumber()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    .line 77
    :cond_3
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getCategory()Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object v1

    const/4 v2, 0x0

    if-eqz v1, :cond_7

    .line 79
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getCategory()Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object v1

    invoke-virtual {v1}, Lnet/gogame/gowrap/support/SupportCategory;->getId()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_4

    .line 80
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    .line 81
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getCategory()Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object v3

    invoke-virtual {v3}, Lnet/gogame/gowrap/support/SupportCategory;->getStringResourceId()I

    move-result v3

    .line 80
    invoke-virtual {v1, v3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v1

    goto :goto_0

    :cond_4
    move-object v1, v2

    :goto_0
    if-nez v1, :cond_5

    .line 84
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    sget v3, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_category_default:I

    invoke-virtual {v1, v3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v1

    :cond_5
    if-eqz v1, :cond_6

    const-string v3, "subject"

    .line 88
    invoke-virtual {v0, v3, v1}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    .line 90
    :cond_6
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getCategory()Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object v1

    invoke-virtual {v1}, Lnet/gogame/gowrap/support/SupportCategory;->getId()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_7

    const-string v1, "category"

    .line 91
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getCategory()Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object v3

    invoke-virtual {v3}, Lnet/gogame/gowrap/support/SupportCategory;->getId()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v3}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    .line 94
    :cond_7
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getBody()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_8

    const-string v1, "body"

    .line 95
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getBody()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v3}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    .line 97
    :cond_8
    sget-object v1, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v1}, Lnet/gogame/gowrap/GoWrapImpl;->getGuid()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_9

    const-string v1, "guid"

    .line 98
    sget-object v3, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v3}, Lnet/gogame/gowrap/GoWrapImpl;->getGuid()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v3}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    :cond_9
    const-string v1, "platform"

    const-string v3, "android"

    .line 100
    invoke-virtual {v0, v1, v3}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "locale"

    .line 101
    sget-object v3, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v3, p0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getCurrentLocale(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v3}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "extraData"

    .line 102
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getName()Ljava/lang/String;

    move-result-object v3

    .line 103
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getCategory()Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object v4

    .line 102
    invoke-static {p0, v3, v4}, Lnet/gogame/gowrap/support/SupportManager;->getExtraData(Landroid/content/Context;Ljava/lang/String;Lnet/gogame/gowrap/support/SupportCategory;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v3}, Lnet/gogame/gowrap/support/MultipartUtility;->addFormField(Ljava/lang/String;Ljava/lang/String;)V

    .line 104
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getAttachment()Landroid/net/Uri;

    move-result-object v1

    if-eqz v1, :cond_a

    .line 105
    invoke-virtual {p0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p0

    .line 106
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getAttachment()Landroid/net/Uri;

    move-result-object p1

    .line 105
    invoke-virtual {p0, p1}, Landroid/content/ContentResolver;->openInputStream(Landroid/net/Uri;)Ljava/io/InputStream;

    move-result-object p0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 108
    :try_start_1
    new-instance p1, Ljava/io/ByteArrayOutputStream;

    invoke-direct {p1}, Ljava/io/ByteArrayOutputStream;-><init>()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 110
    :try_start_2
    invoke-static {p0, p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->copy(Ljava/io/InputStream;Ljava/io/OutputStream;)J

    const-string v1, "attachment"

    .line 111
    invoke-virtual {p1}, Ljava/io/ByteArrayOutputStream;->toByteArray()[B

    move-result-object v3

    invoke-virtual {v0, v1, v3}, Lnet/gogame/gowrap/support/MultipartUtility;->addFilePart(Ljava/lang/String;[B)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 113
    :try_start_3
    invoke-static {p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    .line 116
    :try_start_4
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V
    :try_end_4
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_1
    .catch Lorg/json/JSONException; {:try_start_4 .. :try_end_4} :catch_0

    goto :goto_1

    :catchall_0
    move-exception v0

    .line 113
    :try_start_5
    invoke-static {p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    .line 114
    throw v0
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    :catchall_1
    move-exception p1

    .line 116
    :try_start_6
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 117
    throw p1

    .line 120
    :cond_a
    :goto_1
    invoke-virtual {v0}, Lnet/gogame/gowrap/support/MultipartUtility;->finish()Ljava/lang/String;

    move-result-object p0

    .line 121
    new-instance p1, Lorg/json/JSONObject;

    invoke-direct {p1, p0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p0, "code"

    .line 125
    invoke-virtual {p1, p0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_b

    const-string p0, "code"

    .line 126
    invoke-virtual {p1, p0}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result p0

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    goto :goto_2

    :cond_b
    move-object p0, v2

    :goto_2
    const-string v0, "message"

    .line 128
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_c

    const-string v0, "message"

    .line 129
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    goto :goto_3

    :cond_c
    move-object v0, v2

    :goto_3
    if-eqz p0, :cond_e

    .line 131
    invoke-virtual {p0}, Ljava/lang/Integer;->intValue()I

    move-result v1

    if-nez v1, :cond_d

    goto :goto_4

    .line 132
    :cond_d
    new-instance p1, Lnet/gogame/gowrap/support/SupportServiceException;

    invoke-virtual {p0}, Ljava/lang/Integer;->intValue()I

    move-result p0

    invoke-direct {p1, p0, v0}, Lnet/gogame/gowrap/support/SupportServiceException;-><init>(ILjava/lang/String;)V

    throw p1

    :cond_e
    :goto_4
    const-string p0, "ticketId"

    .line 134
    invoke-virtual {p1, p0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_f

    const-string p0, "ticketId"

    .line 135
    invoke-virtual {p1, p0}, Lorg/json/JSONObject;->getLong(Ljava/lang/String;)J

    move-result-wide p0

    invoke-static {p0, p1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2
    :try_end_6
    .catch Ljava/io/IOException; {:try_start_6 .. :try_end_6} :catch_1
    .catch Lorg/json/JSONException; {:try_start_6 .. :try_end_6} :catch_0

    :cond_f
    return-object v2

    :catch_0
    move-exception p0

    .line 141
    new-instance p1, Lnet/gogame/gowrap/support/SupportServiceException;

    invoke-direct {p1, p0}, Lnet/gogame/gowrap/support/SupportServiceException;-><init>(Ljava/lang/Throwable;)V

    throw p1

    :catch_1
    move-exception p0

    .line 139
    new-instance p1, Lnet/gogame/gowrap/support/SupportServiceException;

    invoke-direct {p1, p0}, Lnet/gogame/gowrap/support/SupportServiceException;-><init>(Ljava/lang/Throwable;)V

    throw p1
.end method
