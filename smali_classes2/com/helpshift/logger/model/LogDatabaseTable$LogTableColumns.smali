.class public Lcom/helpshift/logger/model/LogDatabaseTable$LogTableColumns;
.super Ljava/lang/Object;
.source "LogDatabaseTable.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/logger/model/LogDatabaseTable;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "LogTableColumns"
.end annotation


# static fields
.field public static final EXTRAS:Ljava/lang/String; = "EXTRAS"

.field public static final LEVEL:Ljava/lang/String; = "LEVEL"

.field public static final MESSAGE:Ljava/lang/String; = "MESSAGE"

.field public static final SDK_VERSION:Ljava/lang/String; = "SDK_VERSION"

.field public static final STACKTRACE:Ljava/lang/String; = "STACKTRACE"

.field public static final TIMESTAMP:Ljava/lang/String; = "TIMESTAMP"


# instance fields
.field final synthetic this$0:Lcom/helpshift/logger/model/LogDatabaseTable;


# direct methods
.method public constructor <init>(Lcom/helpshift/logger/model/LogDatabaseTable;)V
    .locals 0

    .line 10
    iput-object p1, p0, Lcom/helpshift/logger/model/LogDatabaseTable$LogTableColumns;->this$0:Lcom/helpshift/logger/model/LogDatabaseTable;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
