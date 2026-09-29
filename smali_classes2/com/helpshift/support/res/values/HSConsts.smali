.class public final Lcom/helpshift/support/res/values/HSConsts;
.super Ljava/lang/Object;
.source "HSConsts.java"


# static fields
.field public static final AGENT_TYPING_INDICATOR_KEY:Ljava/lang/String; = "atai_v2"

.field public static final CORRECT_LANGUAGE_CODE_KEY:Ljava/lang/String; = "clc"

.field public static final ENABLE_FULL_PRIVACY_KEY:Ljava/lang/String; = "fp"

.field public static final ISSUE_ARCHIVAL_KEY:Ljava/lang/String; = "ia"

.field public static final READ_STATUS_KEY:Ljava/lang/String; = "rs"

.field public static final SDK_META:Ljava/lang/String; = "sm"

.field public static final invalidLogins:[Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    const-string v0, ""

    const-string v1, "null"

    const/4 v2, 0x0

    .line 8
    filled-new-array {v2, v0, v1}, [Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/res/values/HSConsts;->invalidLogins:[Ljava/lang/String;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 3
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
