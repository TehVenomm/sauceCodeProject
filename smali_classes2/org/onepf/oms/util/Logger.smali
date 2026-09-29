.class public final Lorg/onepf/oms/util/Logger;
.super Ljava/lang/Object;
.source "Logger.java"


# static fields
.field public static final LOG_TAG:Ljava/lang/String; = "OpenIAB"

.field private static logTag:Ljava/lang/String; = "OpenIAB"
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation
.end field

.field private static loggable:Z


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 34
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static d(Ljava/lang/String;)V
    .locals 2

    .line 95
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x3

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 96
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    invoke-static {v0, p0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    return-void
.end method

.method public static varargs d([Ljava/lang/Object;)V
    .locals 2
    .param p0    # [Ljava/lang/Object;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 77
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x3

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 78
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const-string v1, ""

    invoke-static {v1, p0}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    return-void
.end method

.method public static dWithTimeFromUp(Ljava/lang/String;)V
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 102
    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return-void
.end method

.method public static varargs dWithTimeFromUp([Ljava/lang/Object;)V
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 107
    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return-void
.end method

.method public static e(Ljava/lang/String;)V
    .locals 2

    .line 124
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x6

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 125
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    invoke-static {v0, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    return-void
.end method

.method public static e(Ljava/lang/String;Ljava/lang/Throwable;)V
    .locals 2

    .line 117
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x6

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 118
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    invoke-static {v0, p0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_1
    return-void
.end method

.method public static varargs e(Ljava/lang/Throwable;[Ljava/lang/Object;)V
    .locals 2
    .param p1    # [Ljava/lang/Object;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 130
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x6

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 131
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const-string v1, ""

    invoke-static {v1, p1}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_1
    return-void
.end method

.method public static varargs e([Ljava/lang/Object;)V
    .locals 2
    .param p0    # [Ljava/lang/Object;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 111
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x6

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 112
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const-string v1, ""

    invoke-static {v1, p0}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    return-void
.end method

.method public static i(Ljava/lang/String;)V
    .locals 2

    .line 89
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x4

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 90
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    invoke-static {v0, p0}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    return-void
.end method

.method public static varargs i([Ljava/lang/Object;)V
    .locals 2
    .param p0    # [Ljava/lang/Object;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 83
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x4

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 84
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const-string v1, ""

    invoke-static {v1, p0}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    return-void
.end method

.method public static init()V
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    return-void
.end method

.method public static isLoggable()Z
    .locals 1

    .line 53
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    return v0
.end method

.method public static setLogTag(Ljava/lang/String;)V
    .locals 1

    .line 73
    invoke-static {p0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string p0, "OpenIAB"

    :cond_0
    sput-object p0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    return-void
.end method

.method public static setLoggable(Z)V
    .locals 0

    .line 60
    sput-boolean p0, Lorg/onepf/oms/util/Logger;->loggable:Z

    return-void
.end method

.method public static varargs v([Ljava/lang/Object;)V
    .locals 2
    .param p0    # [Ljava/lang/Object;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 148
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x2

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 149
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const-string v1, ""

    invoke-static {v1, p0}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    return-void
.end method

.method public static w(Ljava/lang/String;)V
    .locals 2

    .line 136
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x5

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 137
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    invoke-static {v0, p0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    return-void
.end method

.method public static w(Ljava/lang/String;Ljava/lang/Throwable;)V
    .locals 2

    .line 142
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x5

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 143
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    invoke-static {v0, p0, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_1
    return-void
.end method

.method public static varargs w([Ljava/lang/Object;)V
    .locals 2
    .param p0    # [Ljava/lang/Object;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 154
    sget-boolean v0, Lorg/onepf/oms/util/Logger;->loggable:Z

    if-nez v0, :cond_0

    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const/4 v1, 0x2

    invoke-static {v0, v1}, Landroid/util/Log;->isLoggable(Ljava/lang/String;I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 155
    :cond_0
    sget-object v0, Lorg/onepf/oms/util/Logger;->logTag:Ljava/lang/String;

    const-string v1, ""

    invoke-static {v1, p0}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    return-void
.end method
