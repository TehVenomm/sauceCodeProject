.class final Lcom/facebook/internal/instrument/InstrumentUtility$2;
.super Ljava/lang/Object;
.source "InstrumentUtility.java"

# interfaces
.implements Ljava/io/FilenameFilter;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/facebook/internal/instrument/InstrumentUtility;->listExceptionReportFiles()[Ljava/io/File;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 156
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public accept(Ljava/io/File;Ljava/lang/String;)Z
    .locals 3

    const-string p1, "^(%s|%s|%s)[0-9]+.json$"

    const/4 v0, 0x3

    .line 159
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "crash_log_"

    const/4 v2, 0x0

    aput-object v1, v0, v2

    const-string v1, "shield_log_"

    const/4 v2, 0x1

    aput-object v1, v0, v2

    const-string v1, "thread_check_log_"

    const/4 v2, 0x2

    aput-object v1, v0, v2

    .line 160
    invoke-static {p1, v0}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    .line 159
    invoke-virtual {p2, p1}, Ljava/lang/String;->matches(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method
